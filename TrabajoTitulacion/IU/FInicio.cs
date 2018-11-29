using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.ModelosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.TiposPersonalizados;
using TrabajoTitulacion.Servicios.Core.Nodos;

namespace TrabajoTitulacion.IU
{
    public partial class FInicio : Form
    {
        public FInicio()
        {
            InitializeComponent();
        }

        private void btnRepositorio_Click(object sender, EventArgs e)
        {
            var mdiParent = MdiParent as FDashboard;
            mdiParent.AbrirRepositorio();
        }

        private void btnGestorModelos_Click(object sender, EventArgs e)
        {
            var mdiParent = MdiParent as FDashboard;
            mdiParent.AbrirGestorModelos();
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            var mdiParent = MdiParent as FDashboard;
            mdiParent.AbrirBusqueda();
        }

        private async void FInicio_Load(object sender, EventArgs e)
        {
            //await pruebaUnitaria();
        }

        private async Task pruebaUnitaria()
        {
            //prueba unitaria
            Model modelo = await ModelosPersonalizadosStatic.ObtenerModeloPersonalizado("testModelNueva");

            Console.WriteLine("N " + modelo.Name);
            Console.WriteLine("M " + modelo.Author);
            Console.WriteLine("S " + modelo.Status);
            //Console.WriteLine("Types: "+modelo.Types.Count);

            List<Modelos.CMM.Type> tipos = await TiposPersonalizadosStatic.ObtenerTiposPersonalizados("testModelNueva");
            foreach (var item in tipos)
            {
                Console.WriteLine("********TIPOS*******");
                Console.WriteLine("Nombre: " + item.Name);
                Console.WriteLine("Descripción: " + item.Description);
                Console.WriteLine("Título" + item.Title);
                foreach (var prop in item.Properties)
                {
                    Console.WriteLine("----*Propiedades*");
                    Console.WriteLine("----Nombre:" + prop.Name);
                    Console.WriteLine("----Título: " + prop.Title);
                    if (prop.Constraints is null)
                    {

                    }
                    else
                    {
                        foreach (var constr in prop.Constraints)
                        {
                            Console.WriteLine("-----------*Constraint*: " + constr.Name);
                            Console.WriteLine("-----------Const PrefixedName:  " + constr.PrefixedName);
                            Console.WriteLine("-------------------*Params*");
                            foreach (var param in constr.Parameters)
                            {
                                Console.WriteLine("----------------- key: " + param.Name + " valor: " + param.SimpleValue);
                            }
                        }

                    }

                }
            }
            List<Aspect> aspectos = await AspectosPersonalizadosStatic.ObtenerAspectosPersonalizados("testModelNueva");
            foreach (var aspecto in aspectos)
            {
                Console.WriteLine();
                Console.WriteLine("********ASPECTOS*******");
                Console.WriteLine("Nombre: " + aspecto.Name);
                foreach (var prop in aspecto.Properties)
                {
                    Console.WriteLine("----*Propiedades*");
                    Console.WriteLine("-----Nombre: " + prop.Name);
                    Console.WriteLine("-----Descripción: " + prop.Description);
                    Console.WriteLine("-----Título: " + prop.Title);
                    Console.WriteLine("-----Tipo de dato: " + prop.Datatype);
                    Console.WriteLine("-----Valor por defecto: " + prop.DefaultValue);
                    Console.WriteLine("-----Prefixed Name:" + prop.PrefixedName);
                    Console.WriteLine("-----Indexación" + prop.IndexTokenisationMode);
                    Console.WriteLine("-----Multivalorado:" + prop.MultiValued);
                    foreach (var constraint in prop.Constraints)
                    {
                        Console.WriteLine("-----------*Constraint*: " + constraint.Name);
                        Console.WriteLine("------------Prefijo: " + constraint.PrefixedName);
                        Console.WriteLine("------------Tipo " + constraint.Type);
                        Console.WriteLine("-------------------*Params*");
                        foreach (var param in constraint.Parameters)
                        {
                            Console.WriteLine("------------------Nombre:" + param.Name);
                            Console.WriteLine("------------------Valor simple:" + param.SimpleValue);
                        }
                    }
                }
            }

        }
    }
}

