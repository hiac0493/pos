using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.DAL.Repository.Domain
{
    public class FacturacionRepository : IFacturacionRepository
    {
        public void GenerateBill(List<ProductosVenta> productosVenta)
        {
            USLib.FachadaCfdv33 usLib = new USLib.FachadaCfdv33();

            usLib.P00Setup(
                numeroDecimalesEnTotales: 2, numeroDecimalesEnDetalle: 6, numeroDecimalesEnImpuestos: 6,
                cerFile: @"D:\Dropbox\CSD Pruebas\CSD_Pruebas_CFDI_MAG041126GT8\CSD_Pruebas_CFDI_MAG041126GT8.cer",
                keyFile: @"D:\Dropbox\CSD Pruebas\CSD_Pruebas_CFDI_MAG041126GT8\CSD_Pruebas_CFDI_MAG041126GT8.key",
                passwordKey: "12345678a");

            usLib.P01DatosGenerales(
                serie: "A", folio: "12345", fecha: DateTime.Now.ToString("s"), formaPago: "01", condicionesDePago: "Contado",
                subTotal: "931.08", descuento: "0.00", moneda: "MXN", tipoCambio: "1.00", total: "997.28", tipoDeComprobante: "I",
                metodoPago: "PUE", lugarExpedicion: "85040", confirmacion: "");

            usLib.P03Emisor(rfc: "MAG041126GT8", nombre: "EMCORSOFT SC", regimenFiscal: "601");

            usLib.P04Receptor(rfc: "XAXX010101000", nombre: "PUBLICO GENERAL", residenciaFiscal: "", numRegIdTrib: "", usoCfdi: "G01");


            var c1 = usLib.P05ConceptosAgregar(
                claveProdServ: "83111504", noIdentificacion: "1001567", cantidad: "5.000000", claveUnidad: "E48", unidad: "Servicio", descripcion: "CONCEPTO 1", valorUnitario: "17.240000", importe: "86.200000", descuento: "0.000000"
            );

            usLib.P05ConceptoAgregarImpuestoTraslado(baseCalculoImpuesto: "86.200000", impuesto: "002", tipoFactor: "Tasa", tasaOCuota: "0.160000", importe: "13.792000", concepto: c1);

            c1 = usLib.P05ConceptosAgregar(
                claveProdServ: "83111504", noIdentificacion: "1001567", cantidad: "1.000000", claveUnidad: "E48", unidad: "Servicio", descripcion: "CONCEPTO 2", valorUnitario: "17.240000", importe: "17.240000", descuento: "0.000000"
            );
            usLib.P05ConceptoAgregarImpuestoTraslado(baseCalculoImpuesto: "17.240000", impuesto: "002", tipoFactor: "Tasa", tasaOCuota: "0.160000", importe: "2.758400", concepto: c1);

            c1 = usLib.P05ConceptosAgregar(
                claveProdServ: "83111504", noIdentificacion: "1001567", cantidad: "2.000000", claveUnidad: "E48", unidad: "Servicio", descripcion: "CONCEPTO 3", valorUnitario: "25.860000", importe: "51.720000", descuento: "0.000000"
            );
            usLib.P05ConceptoAgregarImpuestoTraslado(baseCalculoImpuesto: "51.720000", impuesto: "002", tipoFactor: "Tasa", tasaOCuota: "0.160000", importe: "8.275200", concepto: c1);

            c1 = usLib.P05ConceptosAgregar(
                claveProdServ: "83111504", noIdentificacion: "1001567", cantidad: "3.000000", claveUnidad: "E48", unidad: "Servicio", descripcion: "CONCEPTO 4", valorUnitario: "258.640000", importe: "775.920000", descuento: "0.000000"
            );
            usLib.P05ConceptoAgregarImpuestoTraslado(baseCalculoImpuesto: "775.920000", impuesto: "002", tipoFactor: "Tasa", tasaOCuota: "0.160000", importe: "124.147200", concepto: c1);

            usLib.P05ConceptoAgregarImpuestoRetencion(baseCalculoImpuesto: "775.920000", impuesto: "001", tipoFactor: "Tasa", tasaOCuota: "0.106667", importe: "82.76505864", concepto: c1);

            //Crea el nodo Comprobante.Impuestos con la información que contiene cada uno de los conceptos.
            usLib.P06ImpuestosCrearResumenPorConceptos();

            usLib.P08GenerarSelloDigital();

            var result = usLib.P09TimbrarDocumento(esPrueba: true, validacionPrevia: false, key: "", referencia: "USLib");

            if (result.OperacionExitosa)
            {
                var rfcProveedor = usLib.ResumenCfdv33.TfdRfcProvCertif;
                var uuid = usLib.ResumenCfdv33.TfdUuid;
                var fechaTimbrado = usLib.ResumenCfdv33.TfdFechaTimbrado;
                var selloSat = usLib.ResumenCfdv33.TfdSelloSat;
                var noCertificadoSat = usLib.ResumenCfdv33.TfdNoCertificadoSat;
                var noCertificadoEmisor = usLib.CsdSerie;
                var cadenaSat = usLib.ResumenCfdv33.TfdCadenaOriginal;

                System.IO.File.WriteAllBytes(@"C:\Wsdl\" + usLib.ResumenCfdv33.TfdUuid + ".jpg", usLib.ResumenCfdv33.QrImagen);

                Console.WriteLine("OK");
                System.IO.File.WriteAllBytes(@"C:\Wsdl\" + usLib.ResumenCfdv33.TfdUuid + ".xml", result.XmlFile);
            }
            else
            {
                System.IO.File.WriteAllBytes(@"C:\Wsdl\Error.xml", result.XmlFile);
                Console.WriteLine(result.MensajeError);
            }

            Console.WriteLine("Fin");
            Console.ReadKey();
        }
    }
}
