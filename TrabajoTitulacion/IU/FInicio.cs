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
                Console.WriteLine("_--------------_Tipos___-------");
                Console.WriteLine("N: " + item.Name);
                Console.WriteLine("D: " + item.Description);
                Console.WriteLine("T" + item.Title);
                foreach (var prop in item.Properties)
                {
                    Console.WriteLine("-----------------Properties");
                    Console.WriteLine("Np: " + prop.Name);
                    Console.WriteLine("Tp: " + prop.Title);
                    if (prop.Constraints is null)
                    {

                    }
                    else
                    {
                        foreach (var constr in prop.Constraints)
                        {
                            Console.WriteLine("Const: " + constr.Name);
                            Console.WriteLine("Const:pfxn:  " + constr.PrefixedName);
                            Console.WriteLine("Const: params: _________________");
                            foreach (var param in constr.Parameters)
                            {
                                Console.WriteLine("key: " + param.Name + " valor: " + param.SimpleValue);
                            }
                        }

                    }

                }
            }
            List<Aspect> aspectos = await AspectosPersonalizadosStatic.ObtenerAspectosPersonalizados("testModelNueva");
            foreach (var aspecto in aspectos)
            {
                Console.WriteLine("**************ASPECTOS*****************");
                Console.WriteLine("An: " + aspecto.Name);
                foreach (var prop in aspecto.Properties)
                {
                    Console.WriteLine("**********Prop aspectos**************");
                    Console.WriteLine("Pan: " + prop.Name);
                    Console.WriteLine("Dan: " + prop.Description);
                    Console.WriteLine("Tan: " + prop.Title);
                    Console.WriteLine("tan: " + prop.Datatype);
                    Console.WriteLine("dfv: " + prop.DefaultValue);
                    Console.WriteLine("pfxname:" + prop.PrefixedName);
                    Console.WriteLine("indextonex" + prop.IndexTokenisationMode);
                    Console.WriteLine("multivalued:" + prop.MultiValued);
                    foreach (var constraint in prop.Constraints)
                    {
                        Console.WriteLine("Constr.name: " + constraint.Name);
                        Console.WriteLine("Constr.prefix: " + constraint.PrefixedName);
                        Console.WriteLine("constr.type: " + constraint.Type);
                        foreach (var param in constraint.Parameters)
                        {
                            Console.WriteLine("param.name" + param.Name);
                            Console.WriteLine("param.simplevalue" + param.SimpleValue);

                        }

                    }
                }
            }

        }
    }
}

