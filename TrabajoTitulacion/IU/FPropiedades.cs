using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoTitulacion.Modelos.CMM;
using TrabajoTitulacion.Modelos.CoreAPI;
using TrabajoTitulacion.Servicios.CMM.AspectosPersonalizados;
using TrabajoTitulacion.Servicios.CMM.TiposPersonalizados;
using TrabajoTitulacion.Servicios.Core.Personas;
using TrabajoTitulacion.Servicios.Group;

namespace TrabajoTitulacion.IU
{
    public partial class FPropiedades : Form
    {
        private Model modelo;
        private dynamic subModelo; //Tipo o Aspecto
        private FGestorModelos fgestorModelos;
        private string proveniente;
        private string tipoDato;
        public FPropiedades()
        {
            InitializeComponent();
        }
        public FPropiedades(FGestorModelos fgestorModelos, Model modelo, object subModelo, string proveniente)
        {
            InitializeComponent();
            this.modelo = modelo;
            this.fgestorModelos = fgestorModelos;
            this.proveniente = proveniente;
            if (proveniente == "TIPOS") this.subModelo = (Modelos.CMM.Type)subModelo;
            if (proveniente == "ASPECTOS") this.subModelo = (Aspect)subModelo;
        }
        private async void FPropiedades_Load(object sender, EventArgs e)
        {
            lnklblModeloNav.Text = modelo.Name;
            lnklblSubmodeloNav.Text = subModelo.Name;
            await PoblarDtgv();
        }

        private async Task PoblarDtgv()
        {
            if (proveniente == "ASPECTOS")
                subModelo = await AspectosPersonalizadosStatic.ObtenerAspectoPersonalizado(modelo.Name, subModelo.Name);
            if (proveniente == "TIPOS")
            {
                subModelo = await TiposPersonalizadosStatic.ObtenerTipoPersonalizado(modelo.Name, subModelo.Name);
            }
            dtgviewDatos.AutoGenerateColumns = false;
            dtgviewDatos.DataSource = subModelo.Properties;
            dtgviewDatos.Columns["clmNombreTipo"].DataPropertyName = "Name";
            dtgviewDatos.Columns["clmEtiquetaPresentacionTipo"].DataPropertyName = "Title";
            dtgviewDatos.Columns["clmTipoDato"].DataPropertyName = "DataType";
            dtgviewDatos.Columns["clmRequisito"].DataPropertyName = "Mandatory";
            dtgviewDatos.Columns["clmValorDefault"].DataPropertyName = "DefaultValue";
        }

        private void lnklblVolverNav_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (proveniente == "TIPOS") fgestorModelos.AbrirTipos(modelo);
            if (proveniente == "ASPECTOS") fgestorModelos.AbrirAspectos(modelo);
        }

        private void btnCerrarPlantilla_Click(object sender, EventArgs e)
        {
            flwlypanelPropiedades.Visible = false;
        }
        private void NuevaPlantilla()
        {
            txtNombre.Enabled = true;
            flwlypanelPropiedades.Visible = true;
            txtNombre.Clear();
            txtTitulo.Clear();
            txtDescripcion.Clear();
            cmbxTipoDato.SelectedItem = "d:text";
            cmbxRequerido.SelectedItem = "Opcional";
            cmbxRestriccion.SelectedItem = "Ninguno";
            cmbxIndexacion.SelectedItem = "Ninguno";
            btnAceptar.Text = "Crear";
            lblEstado.Text = "Crear";
            txtValorPredeterminad.Clear();
            lblRestriccionInf.Visible = false;
            lblRestriccionSup.Visible = false;
            txtRestriccionInf.Visible = false;
            txtRestriccionSup.Visible = false;
            lblExpresionRegular.Visible = false;
            txtExpresionRegular.Visible = false;
            chkValorPredeterminado.Checked = false;
            chkValorPredeterminado.Enabled = true;
            cmbxValorPredeterminado.Enabled = true;
            txtValorPredeterminad.Enabled = true;
            dtpValorPredeterminado.Enabled = true;
            cmbxValorPredeterminado.Visible = false;
            txtValorPredeterminad.Visible = false;
            dtpValorPredeterminado.Visible = false;
            btnAceptar.Enabled = true;

        }
        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            bool sinProblemas = true;
            // valido que el campo txtNombre contenga algún valor, ya que este campo es obligatorio
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Es obligatorio llenar el campo: Nombre ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            // si el campo txtNombre tiene un valor
            else
            {

                ///////////////////////////////////////validacion tipo de Datos ////////////////////////////////////////////

                //verifica si el valor ingresado en txtValorPredeterminad es un entero
                if (tipoDato == "d:int")
                {

                    int numeroEntero;

                    bool esEntero = Int32.TryParse(txtValorPredeterminad.Text, out numeroEntero);

                    if (!esEntero)
                    {
                        MessageBox.Show("Ha ingresado un valor Predeterminado incorrecto, debe de ser un número Entero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        sinProblemas = false;
                    }
                    else
                    {
                        sinProblemas = true;
                    }


                }

                //verifica si el valor ingresado en txtValorPredeterminad es float
                else if (tipoDato == "d:float")
                {

                    float numeroFloat;

                    bool esFloat = float.TryParse(txtValorPredeterminad.Text, out numeroFloat);

                    if (!esFloat)
                    {
                        MessageBox.Show("Ha ingresado un valor Predeterminado incorrecto, debe de ser del tipo float", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        sinProblemas = false;
                    }

                    else
                    {
                        sinProblemas = true;
                    }

                }

                //verifica si el valor ingresado en txtValorPredeterminad es double
                else if (tipoDato == "d:double")
                {

                    double numeroDouble;

                    bool esDouble = Double.TryParse(txtValorPredeterminad.Text, out numeroDouble);

                    if (!esDouble)
                    {
                        MessageBox.Show("Ha ingresado un valor Predeterminado incorrecto, debe de ser del tipo double", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        sinProblemas = false;
                    }
                    else
                    {
                        sinProblemas = true;
                    }


                }

                //verifica si el valor ingresado en txtValorPredeterminad es long
                else if (tipoDato == "d:long")
                {

                    long numeroLong;

                    bool esLong = long.TryParse(Console.ReadLine(), out numeroLong);

                    if (!esLong)
                    {
                        MessageBox.Show("Ha ingresado un valor Predeterminado incorrecto, debe de ser del tipo long", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        sinProblemas = false;
                    }
                    else
                    {
                        sinProblemas = true;
                    }

                }
                else if (tipoDato == "string") //tipoDato == "d:text" || tipoDato== "d:mltext"
                {
                    sinProblemas = true;
                }

                ///////////////////////////////////////////////////// validación de las restricciones //////////////////////////////////////

                if (cmbxRestriccion.SelectedItem.ToString() == "Ninguno" && sinProblemas) await CrearActualizarPropiedad();
                else if (cmbxRestriccion.SelectedItem.ToString() == "Expresión Regular" && sinProblemas)
                {
                    await CrearActualizarPropiedad();
                }
                else if (cmbxRestriccion.SelectedItem.ToString() == "Longitud Mínima/Máxima" && sinProblemas)
                {
                    string tipoDatoRestr = cmbxTipoDato.SelectedItem.ToString();

                    if (tipoDatoRestr == "d:text" || tipoDatoRestr == "d:mltext")
                    {
                        if (!string.IsNullOrEmpty(txtRestriccionInf.Text) || !string.IsNullOrEmpty(txtRestriccionSup.Text))
                        {

                            
                            int longitudMinima = Int32.Parse(txtRestriccionInf.Text);
                            int longitudMaxima = Int32.Parse(txtRestriccionSup.Text);

                            if (longitudMinima > longitudMaxima)
                            {
                                MessageBox.Show("La longitud mínima debe de ser menor que la longitud máxima", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                if(txtValorPredeterminad.Text != "")
                                {
                                    int longitudValorPredeterminado = Int32.Parse(txtValorPredeterminad.Text);
                                    if (longitudValorPredeterminado >= longitudMinima && longitudValorPredeterminado <= longitudMaxima)
                                    {
                                        await CrearActualizarPropiedad();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Longitud de: " + txtValorPredeterminad.Text + " no está en el rango [" + longitudMinima + "," + longitudMaxima + "]", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else
                                {
                                    await CrearActualizarPropiedad();
                                }
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ingrese la longitud mínima y máxima", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Solo se puede usar la Restricción LENGTH con tipo de dato TEXT y MLTEXT", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                else if (cmbxRestriccion.SelectedItem.ToString() == "Valor Mínimo/Máximo" && sinProblemas)
                {
                    string tipoDatoRestr = cmbxTipoDato.SelectedItem.ToString();

                    if (tipoDatoRestr == "d:int" || tipoDatoRestr == "d:float" || tipoDatoRestr == "d:double" || tipoDatoRestr == "d:long")
                    {

                        if (!string.IsNullOrEmpty(txtRestriccionInf.Text) || !string.IsNullOrEmpty(txtRestriccionSup.Text))
                        {
                            
                            int valorMinimo = Int32.Parse(txtRestriccionInf.Text);
                            int valorMaximo = Int32.Parse(txtRestriccionSup.Text);

                            if (valorMinimo > valorMaximo)
                            {
                                MessageBox.Show("El valor mínimo debe de ser menor que el valor máximo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                if(txtValorPredeterminad.Text != null)
                                {
                                    int valorValorPredeterminado = Int32.Parse(txtValorPredeterminad.Text);
                                    if (valorValorPredeterminado >= valorMinimo && valorValorPredeterminado <= valorMaximo)
                                    {
                                        await CrearActualizarPropiedad();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Valor numérico : " + txtValorPredeterminad.Text + " no está en el rango [" + valorMinimo + "," + valorMaximo + "]", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else
                                {
                                    await CrearActualizarPropiedad();
                                }
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("Los campos máximo y mínimo deben contener un valor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Solo se puede usar la Restricción VALUE con tipo de dato INT, FLOAT. DOUBLE, LONG", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }



            }
        }

        public bool VerificarExpresioRegular(string expresionPrueba)
        {
            bool esValido = true;
            if ((expresionPrueba != null) && (expresionPrueba.Trim().Length > 0))
            {
                try
                {
                    Regex.Match("", expresionPrueba);
                }
                catch (ArgumentException)
                {
                    // BAD PATTERN: Syntax error
                    esValido = false;
                }
            }
            else
            {
                //BAD PATTERN: expresion es null o vacio
                esValido = false;
            }
            return esValido;
        }
        private async Task CrearActualizarPropiedad()
        {

            if (btnAceptar.Text == "Crear")
            {
                List<Modelos.CMM.Constraint> constraints = new List<Modelos.CMM.Constraint>();
                cmbxIndexacion.SelectedItem = "Ninguno";
                Property propiedadCrear = new Property();
                propiedadCrear.Name = txtNombre.Text;
                propiedadCrear.Description = txtDescripcion.Text;
                propiedadCrear.Title = txtTitulo.Text;
                propiedadCrear.Datatype = cmbxTipoDato.SelectedItem.ToString();
                propiedadCrear.MultiValued = false;

                string tipoDatoCrear = cmbxTipoDato.SelectedItem.ToString();
                if (tipoDatoCrear == "d:int" || tipoDatoCrear == "d:float" || tipoDatoCrear == "d:double" || tipoDatoCrear == "d:long" || tipoDatoCrear == "d:mltext" || tipoDatoCrear == "d:text")
                {
                    propiedadCrear.DefaultValue = txtValorPredeterminad.Text;    //////

                }
                else if (tipoDatoCrear == "d:boolean")
                {
                    if (cmbxValorPredeterminado.SelectedItem.ToString() == "Verdadero")
                        propiedadCrear.DefaultValue = "true";
                    else if (cmbxValorPredeterminado.SelectedItem.ToString() == "Falso")
                        propiedadCrear.DefaultValue = "false";

                }
                else if (tipoDato == "date")
                {

                    propiedadCrear.DefaultValue = dtpValorPredeterminado.Value.ToString("yyyy-MM-dd");
                }

                if (cmbxRequerido.SelectedItem.ToString() == "Opcional") propiedadCrear.Mandatory = false;
                else { propiedadCrear.Mandatory = true; }

                if (cmbxRestriccion.SelectedItem.ToString() == "Ninguno") propiedadCrear.Constraints = null;
                else if (cmbxRestriccion.SelectedItem.ToString() == "Expresión Regular")
                {
                    if (!string.IsNullOrEmpty(txtExpresionRegular.Text))
                    {

                        if (!VerificarExpresioRegular(txtExpresionRegular.Text))
                        {
                            MessageBox.Show("Ingrese una Expresión Regular válida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtExpresionRegular.Clear();
                            return;
                        }
                        else
                        {
                            Modelos.CMM.Constraint constraintRegex = new Modelos.CMM.Constraint("");
                            constraintRegex.Type = "REGEX";
                            List<Parameter> parameters = new List<Parameter>();
                            Parameter parameter1 = new Parameter();
                            parameter1.Name = "expression";
                            parameter1.SimpleValue = txtExpresionRegular.Text;
                            parameters.Add(parameter1);

                            Parameter parameter2 = new Parameter();
                            parameter2.Name = "requiresMatch";
                            parameter2.SimpleValue = "true";
                            parameters.Add(parameter2);
                            constraintRegex.Parameters = parameters;

                            constraints.Add(constraintRegex);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese una Expresión Regular", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }



                }
                else if (cmbxRestriccion.SelectedItem.ToString() == "Longitud Mínima/Máxima")
                {
                    Modelos.CMM.Constraint constraintRegex = new Modelos.CMM.Constraint("");
                    constraintRegex.Type = "LENGTH";
                    List<Parameter> parameters = new List<Parameter>();
                    Parameter parameter1 = new Parameter();
                    parameter1.Name = "minLength";
                    parameter1.SimpleValue = txtRestriccionInf.Text;
                    parameters.Add(parameter1);

                    Parameter parameter2 = new Parameter();
                    parameter2.Name = "maxLength";
                    parameter2.SimpleValue = txtRestriccionSup.Text;
                    parameters.Add(parameter2);
                    constraintRegex.Parameters = parameters;

                    constraints.Add(constraintRegex);

                }
                else if (cmbxRestriccion.SelectedItem.ToString() == "Valor Mínimo/Máximo")
                {
                    if (Int32.Parse(txtValorPredeterminad.Text) >= Int32.Parse(txtRestriccionInf.Text) && Int32.Parse(txtValorPredeterminad.Text) <= Int32.Parse(txtRestriccionSup.Text))
                    {
                        Modelos.CMM.Constraint constraintRegex = new Modelos.CMM.Constraint("");
                        constraintRegex.Type = "MINMAX";
                        List<Parameter> parameters = new List<Parameter>();
                        Parameter parameter1 = new Parameter();
                        parameter1.Name = "minValue";
                        parameter1.SimpleValue = txtRestriccionInf.Text;
                        parameters.Add(parameter1);

                        Parameter parameter2 = new Parameter();
                        parameter2.Name = "maxValue";
                        parameter2.SimpleValue = txtRestriccionSup.Text;
                        parameters.Add(parameter2);
                        constraintRegex.Parameters = parameters;

                        constraints.Add(constraintRegex);
                    }
                    else
                    {
                        MessageBox.Show("Valor predeterminado no corresponde a la restricción Valor Mínimo/Máximo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                }

                if (cmbxIndexacion.SelectedItem.ToString() == "Ninguno")
                {
                    propiedadCrear.Facetable = "UNSET";
                    propiedadCrear.IndexTokenisationMode = "TRUE";
                    propiedadCrear.Indexed = false;
                    propiedadCrear.MandatoryEnforced = false;
                }else if(cmbxIndexacion.SelectedItem.ToString() == "Texto libre")
                {
                    propiedadCrear.Facetable = "FALSE";
                    propiedadCrear.IndexTokenisationMode = "TRUE";
                    propiedadCrear.Indexed = true;
                    propiedadCrear.MandatoryEnforced = false;
                }

                propiedadCrear.Constraints = constraints;

                List<Property> propiedadesCrear = new List<Property>();
                propiedadesCrear.Add(propiedadCrear);
                PropertiesBodyUpdate propertiesBodyCreate = new PropertiesBodyUpdate(subModelo.Name, propiedadesCrear);
                if (proveniente == "ASPECTOS")
                {
                    await AspectosPersonalizadosStatic.AñadirPropiedadeAspecto(
                        modelo.Name,
                        subModelo.Name,
                        propertiesBodyCreate);
                    MessageBox.Show("Propiedad creada exitosamente");
                }
                if (proveniente == "TIPOS") fgestorModelos.AbrirAspectos(modelo);
                await PoblarDtgv();
                NuevaPlantilla();


            }
            else if (btnAceptar.Text == "Editar")
            {

                if (modelo.Status == "DRAFT")
                {
                    Property propiedadEditar = new Property();
                    propiedadEditar.Name = txtNombre.Text;
                    propiedadEditar.Description = txtDescripcion.Text;
                    propiedadEditar.Title = txtTitulo.Text;
                    propiedadEditar.Datatype = cmbxTipoDato.SelectedItem.ToString();

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    string tipoDatoEditar = cmbxTipoDato.SelectedItem.ToString();
                    if (tipoDatoEditar == "d:int" || tipoDatoEditar == "d:float" || tipoDatoEditar == "d:double" || tipoDatoEditar == "d:long" || tipoDatoEditar == "d:mltext" || tipoDatoEditar == "d:text")
                    {
                        propiedadEditar.DefaultValue = txtValorPredeterminad.Text;

                    }
                    else if (tipoDato == "d:boolean")
                    {
                        if (cmbxValorPredeterminado.SelectedItem.ToString() == "Verdadero")
                        {
                            propiedadEditar.DefaultValue = "true";
                        }
                        else if (cmbxValorPredeterminado.SelectedItem.ToString() == "Falso")
                        {
                            propiedadEditar.DefaultValue = "false";

                        }

                    }
                    else if (tipoDato == "d:date")
                    {

                        propiedadEditar.DefaultValue = dtpValorPredeterminado.Value.ToString();
                    }

                    if (cmbxRequerido.SelectedItem.ToString() == "Opcional") propiedadEditar.Mandatory = false;
                    else { propiedadEditar.Mandatory = true; }
                    if (cmbxRequerido.SelectedItem.ToString() == "Ninguno") propiedadEditar.Constraints = null;
                    else
                    {
                    }
                    if (cmbxIndexacion.SelectedItem.ToString() == "Ninguno")
                    {
                        propiedadEditar.Facetable = "UNSET";
                        propiedadEditar.IndexTokenisationMode = "TRUE";
                        propiedadEditar.Indexed = false;
                        propiedadEditar.MandatoryEnforced = false;
                    }
                    List<Property> propiedades = new List<Property>();
                    propiedades.Add(propiedadEditar);
                    PropertiesBodyUpdate propertiesBodyUpdate = new PropertiesBodyUpdate(subModelo.Name, propiedades);

                    if (proveniente == "ASPECTOS")
                    {
                        await AspectosPersonalizadosStatic.ActualizarPropiedadAspecto(
                        modelo.Name,
                        subModelo.Name,
                        propiedadEditar.Name,
                        propertiesBodyUpdate);
                        MessageBox.Show("Propiedad actualizada exitosamente");
                    }
                    if (proveniente == "TIPOS") fgestorModelos.AbrirAspectos(modelo);

                    await PoblarDtgv();
                    NuevaPlantilla();
                }
                else
                {
                    MessageBox.Show("Para Editar una propiedad el modelo debe estar desactivado");
                }
            }
        }

        private async void tlstripCrear_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                NuevaPlantilla();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void tlstripEditar_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                PlantillaEditar();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtgviewDatos_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    var indicesDtgviewDatos = dtgviewDatos.HitTest(e.X, e.Y);
                    dtgviewDatos.ClearSelection();
                    dtgviewDatos.Rows[indicesDtgviewDatos.RowIndex].Selected = true;
                    dtgviewDatos.ContextMenuStrip = cntxMenuAcciones;
                }
                catch (ArgumentOutOfRangeException)
                {
                    dtgviewDatos.ContextMenuStrip = cntxMenuGeneral;
                }
            }
        }

        private void PlantillaEditar()
        {
            Property propiedadEditar = new Property();
            if (proveniente == "ASPECTOS")
            {
                Aspect aspectoActual = (Aspect)subModelo;
                propiedadEditar = (from propiedad in aspectoActual.Properties
                                   where propiedad.Name == dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString()
                                   select propiedad).FirstOrDefault();
            }
            else if (proveniente == "TIPOS")
            {
                Modelos.CMM.Type aspectoActual = (Modelos.CMM.Type)subModelo;
                propiedadEditar = (from propiedad in aspectoActual.Properties
                                   where propiedad.Name == dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString()
                                   select propiedad).FirstOrDefault();
            }

            ValidacionSegunDataType(propiedadEditar);

            flwlypanelPropiedades.Visible = true;
            btnAceptar.Text = "Editar";
            lblEstado.Text = "Editando Propiedad";
            txtNombre.Text = propiedadEditar.Name;
            txtNombre.Enabled = false;
            txtTitulo.Text = propiedadEditar.Title;
            txtDescripcion.Text = propiedadEditar.Description;
            cmbxTipoDato.SelectedIndex = cmbxTipoDato.Items.IndexOf(propiedadEditar.Datatype);
            if (propiedadEditar.Mandatory)
                cmbxRequerido.SelectedIndex = cmbxRequerido.Items.IndexOf("Obligatorio");
            else cmbxRequerido.SelectedIndex = cmbxRequerido.Items.IndexOf("Opcional");
            if (propiedadEditar.Constraints is null)
                cmbxRestriccion.SelectedIndex = cmbxRestriccion.Items.IndexOf("Ninguno");
            else { }
            if (propiedadEditar.Indexed)
                cmbxIndexacion.SelectedIndex = cmbxIndexacion.Items.IndexOf("Ninguno");
        }

        private void ValidacionSegunDataType(Property propiedadEditar)
        {
            //Validación:
            string tipoDatoEdit = propiedadEditar.Datatype;

            if (tipoDatoEdit == "d:int" || tipoDatoEdit == "d:float" || tipoDatoEdit == "d:double" || tipoDatoEdit == "d:long" || tipoDatoEdit == "d:mltext" || tipoDatoEdit == "d:text")
            {
                if (propiedadEditar.DefaultValue != null)
                {
                    txtValorPredeterminad.Text = propiedadEditar.DefaultValue;
                }

            }
            else if (tipoDatoEdit == "d:boolean")
            {
                if (propiedadEditar.DefaultValue != null)
                {
                    if (propiedadEditar.DefaultValue == "true")
                        cmbxValorPredeterminado.SelectedIndex = cmbxValorPredeterminado.Items.IndexOf("Verdadero");
                    else if (propiedadEditar.DefaultValue == "false")
                        cmbxValorPredeterminado.SelectedIndex = cmbxValorPredeterminado.Items.IndexOf("Falso");
                }

            }
            else if (tipoDatoEdit == "d:date")
            {
                if (propiedadEditar.DefaultValue != null)
                {
                    DateTime fecha;
                    DateTime.TryParse(propiedadEditar.DefaultValue, out fecha);
                    dtpValorPredeterminado.Value = fecha;
                }

            }
        }

        private async void tlstripEliminar_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                FLoading fPrincipalLoading = new FLoading();
                fPrincipalLoading.Show();
                await AspectosPersonalizadosStatic.EliminarPropiedadAspecto(
                    modelo.Name,
                    subModelo.Name,
                    dtgviewDatos.SelectedRows[0].Cells[0].Value.ToString());
                fPrincipalLoading.Close();
                MessageBox.Show("La propiedad ha sido eliminada");
                await PoblarDtgv();
                dtgviewDatos.Refresh();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Validaciones:
        private void cmbxTipoDato_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbxIndexacion.SelectedItem = "Ninguno"; ///
            cmbxIndexacion.Items.Clear();
            txtValorPredeterminad.Clear();
            txtValorPredeterminad.Mask = "";

            chkValorPredeterminado.Checked = true;
            String valorTipoDato = cmbxTipoDato.SelectedItem.ToString();
            if (valorTipoDato.Equals("d:text") || valorTipoDato.Equals("d:mltext"))
            {
                // muestro solo el textbox de Valor Predeterminado, los demás elementos están ocultos
                dtpValorPredeterminado.Visible = false;
                txtValorPredeterminad.Visible = true;
                cmbxValorPredeterminado.Visible = false;


                //cargo los valores de los items en el combobox  cmbxIndexacion que corresponden a texto
                cmbxIndexacion.Items.Add("Ninguno");
                cmbxIndexacion.Items.Add("Texto libre");
                cmbxIndexacion.Items.Add("Lista de valores - coincidencia completa");
                cmbxIndexacion.Items.Add("Lista de valores - coincidencia parcial");
                cmbxIndexacion.Items.Add("Patrón - coincidencias únicas");
                cmbxIndexacion.Items.Add("Patrón - muchas coincidencias");

                tipoDato = "string";

            }
            else if (valorTipoDato.Equals("d:int") || valorTipoDato.Equals("d:long") || valorTipoDato.Equals("d:float") || valorTipoDato.Equals("d:double")
                 || valorTipoDato.Equals("d:date"))
            {

                // muestro solo el textbox de Valor Predeterminado, los demás elementos están ocultos
                dtpValorPredeterminado.Visible = false;
                txtValorPredeterminad.Visible = true;
                cmbxValorPredeterminado.Visible = false;

                //cargo los valores de los items en el combobox  cmbxIndexacion que corresponden a datos tipo numérico y date
                cmbxIndexacion.Items.Add("Ninguno");
                cmbxIndexacion.Items.Add("Básico");
                cmbxIndexacion.Items.Add("Busqueda mejorada");

                if (valorTipoDato.Equals("d:date"))
                {
                    tipoDato = "date";

                    // muestro solo dateTimePicker de Valor Predeterminado, los demás elementos están ocultos
                    dtpValorPredeterminado.Visible = true;
                    txtValorPredeterminad.Visible = false;
                    cmbxValorPredeterminado.Visible = false;


                }
                else if (valorTipoDato.Equals("d:int"))
                {
                    tipoDato = "int";

                }
                else if (valorTipoDato.Equals("d:float"))
                {
                    tipoDato = "float";
                }
                else if (valorTipoDato.Equals("d:long"))
                {
                    tipoDato = "long";
                }
                else if (valorTipoDato.Equals("d:double"))
                {
                    tipoDato = "double";
                }
            }
            else if (valorTipoDato.Equals("d:boolean"))
            {
                cmbxIndexacion.Items.Add("Ninguno");
                cmbxIndexacion.Items.Add("Básico");

                cmbxValorPredeterminado.SelectedItem = "Falso";
                tipoDato = "boolean";

                // muestro solo el combobox de Valor Predeterminado, los demás elementos están ocultos
                dtpValorPredeterminado.Visible = false;
                txtValorPredeterminad.Visible = false;
                cmbxValorPredeterminado.Visible = true;

            }

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {

            //permite el ingreso solo de numeros, letras mayusculas y minusculas, tecla de borrar , signo - y _
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar == 8) || e.KeyChar == '-' || e.KeyChar == '_')
                e.Handled = false;
            else
            {
                MessageBox.Show("Utilice números, letras, guiones (-) y guiones bajos (_) solamente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtValorPredeterminad_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            //validación de que el número ingresado sea un nùmero double o float (positivo o negativo)

            if (tipoDato == "double" || tipoDato == "float" || tipoDato == "long")
            {


                if (string.IsNullOrEmpty(txtValorPredeterminad.Text))
                {

                    // si el primer valor ingresado es un punto '.' lanza el mensaje de advertencia
                    if (ch == '.')
                    {
                        e.Handled = true;
                        MessageBox.Show("Debe de ingresar un número", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                // !Char.IsDigit(ch) --> si es diferente a un número
                // ASCII 8 --> backspace o borrar
                if (!Char.IsDigit(ch) && ch != 8 && ch != '.' && ch != '-')
                {
                    e.Handled = true;
                }


                if (ch == '-')
                {
                    //si el signo menos '-' no es ingresado al inicio, no aceptar el signo 
                    if (!string.IsNullOrEmpty(txtValorPredeterminad.Text))
                    {
                        e.Handled = true;
                        return;

                    }
                    if (!string.IsNullOrEmpty(txtValorPredeterminad.Text))
                    {
                        e.Handled = true;
                        return;

                    }
                    //si el signo menos '-' ya ha sido ingresado una vez, ya no volver a aceptar 
                    else if (ch == '-' && txtValorPredeterminad.Text.IndexOf('-') != -1)
                    {
                        e.Handled = true;
                        return;

                    }
                }

                //validar el ingreso de solo un punto en todo el número
                if (ch == '.' && txtValorPredeterminad.Text.IndexOf('.') != -1)
                {
                    e.Handled = true;
                    return;
                }

            }


            // validación de que el dato ingresado sea un número int (positivo o negativo)

            if (tipoDato == "int")
            {
                // !Char.IsDigit(ch) --> si es diferente a un número
                // ASCII 8 --> backspace o borrar
                if (!Char.IsDigit(ch) && ch != 8 && ch != '-')
                {
                    e.Handled = true;
                }

                if (ch == '-')
                {
                    //si el signo menos '-' no es ingresado al inicio, no aceptar el signo 
                    if (!string.IsNullOrEmpty(txtValorPredeterminad.Text))
                    {
                        e.Handled = true;
                        return;

                    }
                    //si el signo menos '-' ya ha sido ingresado una vez, ya no volver a aceptar 
                    else if (ch == '-' && txtValorPredeterminad.Text.IndexOf('-') != -1)
                    {
                        e.Handled = true;
                        return;

                    }
                }
            }

        }

        private void cmbxRestriccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxRestriccion.SelectedItem.ToString() == "Ninguno")
            {

                lblRestriccionInf.Visible = false;
                lblRestriccionSup.Visible = false;
                txtRestriccionInf.Visible = false;
                txtRestriccionSup.Visible = false;
                lblExpresionRegular.Visible = false;
                txtExpresionRegular.Visible = false;


            }
            else if (cmbxRestriccion.SelectedItem.ToString() == "Expresión Regular")
            {
                if (tipoDato != "string")
                {
                    MessageBox.Show("Expresiones regulares solamente con tipo de datos d:text y d:mltext", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbxRestriccion.SelectedItem = "Ninguno";
                }
                else
                {
                    lblRestriccionInf.Visible = false;
                    lblRestriccionSup.Visible = false;
                    txtRestriccionInf.Visible = false;
                    txtRestriccionSup.Visible = false;
                    lblExpresionRegular.Visible = true;
                }
            }
            else if (cmbxRestriccion.SelectedItem.ToString() == "Longitud Mínima/Máxima")
            {
                if (!(tipoDato == "string" || tipoDato == "int" || tipoDato == "d:float" || tipoDato == "d:double" || tipoDato == "d:long"))
                {
                    MessageBox.Show("Longitud Mínima/Máxima solamente con tipo de datos textuales y numéricos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbxRestriccion.SelectedItem = "Ninguno";
                }
                else
                {
                    txtRestriccionInf.Visible = true;
                    txtRestriccionSup.Visible = true;
                    lblExpresionRegular.Visible = false;
                    lblRestriccionInf.Visible = true;
                    lblRestriccionSup.Visible = true;

                    lblRestriccionInf.Text = "Longitud mínima";
                    lblRestriccionInf.Text = "Longitud máxima";
                    txtRestriccionInf.Text = "0";
                    txtRestriccionSup.Text = "256";
                }
            }
            else if (cmbxRestriccion.SelectedItem.ToString() == "Valor Mínimo/Máximo")
            {
                if (!(tipoDato == "int" || tipoDato == "d:float" || tipoDato == "d:double" || tipoDato == "d:long"))
                {
                    MessageBox.Show("Valor Mínimo/Máximo solamente con tipo de datos numéricos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cmbxRestriccion.SelectedItem = "Ninguno";
                }
                else
                {
                    txtRestriccionInf.Visible = true;
                    txtRestriccionSup.Visible = true;
                    lblExpresionRegular.Visible = false;
                    lblRestriccionInf.Visible = true;
                    lblRestriccionSup.Visible = true;

                    lblRestriccionInf.Text = "Valor mínimo";
                    lblRestriccionInf.Text = "Valor máximo";
                    txtRestriccionInf.Text = "0";
                    txtRestriccionSup.Text = "10";

                }
            }
            else
            {


            }
        }

        //validar que solo se ingresen números en la restriccion de límite max o min y valor max o min
        private void txtRestriccionInf_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            // !Char.IsDigit(ch) --> si es diferente a un número
            // ASCII 8 --> backspace o borrar
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("Ingrese solo números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        //validar que solo se ingresen números en la restriccion de límite max o min y valor max o min
        private void txtRestriccionSup_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            // !Char.IsDigit(ch) --> si es diferente a un número
            // ASCII 8 --> backspace o borrar
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("Ingrese solo números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void lnklblModeloNav_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (proveniente == "TIPOS") fgestorModelos.AbrirTipos(modelo);
            if (proveniente == "ASPECTOS") fgestorModelos.AbrirAspectos(modelo);
        }

        private void chkValorPredeterminado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkValorPredeterminado.Checked)
            {
                if (tipoDato == "date" || tipoDato == "datetime")
                {
                    dtpValorPredeterminado.Visible = true;
                }
                else if (tipoDato == "boolean")
                {
                    cmbxValorPredeterminado.Visible = true;
                }
                else
                {
                    txtValorPredeterminad.Visible = true;
                }
            }
            else
            {
                cmbxValorPredeterminado.Visible = false;
                txtValorPredeterminad.Visible = false;
                dtpValorPredeterminado.Visible = false;
                cmbxValorPredeterminado.SelectedItem = null;
                txtValorPredeterminad.Text = null;
            }
        }

        private void cmbxRequerido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxRequerido.SelectedItem.ToString() == "Obligatorio")
            {
                chkValorPredeterminado.Checked = false;
                chkValorPredeterminado.Enabled = false;
                cmbxValorPredeterminado.Enabled = false;
                txtValorPredeterminad.Enabled = false;
                dtpValorPredeterminado.Enabled = false;
            }
            else
            {
                chkValorPredeterminado.Enabled = true;
                cmbxValorPredeterminado.Enabled = true;
                txtValorPredeterminad.Enabled = true;
                dtpValorPredeterminado.Enabled = true;
            }
        }

        private async void tlstripCrearAspecto_Click(object sender, EventArgs e)
        {
            List<GroupMember> groupMembers = await GruposStatic.ObtenerMiembrosGrupoAdministradorModelos();
            if (!(groupMembers.Find(x => x.Id == PersonasStatic.PersonaAutenticada.Id) is null))
            {
                NuevaPlantilla();
            }
            else
            {
                MessageBox.Show("No tiene los permisos suficientes para realizar esta acción. Usted no pertenece al grupo ALFRESCO_MODEL_ADMINISTRATORS.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtExpresionRegular_Leave(object sender, EventArgs e)
        {

        }

    }
}
