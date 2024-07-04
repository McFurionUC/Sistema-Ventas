using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CapaNegocio;

namespace CapaEntidad.EntidadBoleta
{
    
    public  class EBoleta
    {

        BoletaBL boleta = new BoletaBL();
        public int NroBoleta { get; set; }
        public string CodCliente { get; set; }
        public string CodProducto { get; set; }
        public string CodVendedor { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime Fecha { get; set; }
        public int Anulado { get; set; }

        public Tuple<string,int,decimal> Buscar()
        {
            
            var resultadoTuple = boleta.Buscar(NroBoleta);
            if(resultadoTuple != null)
            {
                return Tuple.Create(resultadoTuple.Item1,resultadoTuple.Item2,resultadoTuple.Item3);
            }
            else
            {
                return resultadoTuple;
            }
            
            
        }
        //Combo Box con datos
        public List<string> ObtenerVendedor()
        {
            return boleta.ObtenerVendedor();
        }
        public List<string> ObtenerCliente()
        {
            return boleta.ObtenerCliente();
        }
        public List<string> ObtenerCategoria()
        {
            return boleta.ObtenerCategoria();
        }
        public List<string> ObtenerProducto()
        {
            return boleta.ObtenerProducto();
        }


        //Obtener Boleta
        public int UltimoNroBoleta()
        {
            return boleta.ObtenerSiguienteNumeroBoleta();
        }
        

        //Metodos Crud 
        public string Agregar()
        {
            return boleta.AgregarProducto(CodCliente, CodProducto, CodVendedor, NroBoleta, Stock);
        }

        public decimal Total()
        {
            return boleta.CalcularTotalPorBoleta(NroBoleta);
        }

    }

}
