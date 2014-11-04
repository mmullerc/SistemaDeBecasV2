﻿Imports EntitiesLayer
Imports System.Windows.Forms
Imports System.Drawing

Public Class uCntrlBuscarBeneficio

    Private Sub PantallaConsultarBeneficio_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            listarBeneficios()

        Catch

        End Try

    End Sub

    ''' <summary>
    ''' Este método llama a un método en el gestor y recibe una lista de beneficios.
    ''' Llena el data grid con la lista de beneficios.
    ''' </summary>
    ''' <author>Mathias Muller</author>
    Public Sub listarBeneficios()


        Try


            Dim listaBeneficios As New List(Of Beneficio)
            listaBeneficios = objGestorBeneficio.buscarBeneficios()
            dtaBuscarBeneficio.Rows.Clear()

            For Each Beneficio In listaBeneficios

                dtaBuscarBeneficio.Rows.Add(Beneficio.Id, Beneficio.Nombre, Beneficio.Porcentaje, Beneficio.Aplicacion)
                dtaBuscarBeneficio.Columns("dtaAplicabilidad").Visible = False
                dtaBuscarBeneficio.Columns("dtaId").Visible = False

            Next

        Catch ex As Exception

            Dim UCtrl As UctrlAlerta = New UctrlAlerta()

            Me.Controls.Add(UCtrl)
            UCtrl.lblAlerta.Text = ex.Message
            UCtrl.Location = New Point(300, 100)
            UCtrl.BringToFront()
            UCtrl.Show()

        End Try



    End Sub

    ''' <summary>
    ''' Este método agarra el valor seleccionado del combobox y crea un evento
    ''' </summary>
    ''' <author>Mathias Muller</author>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dtaBuscarBeneficio_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dtaBuscarBeneficio.EditingControlShowing
        ' Only for a DatagridComboBoxColumn at ColumnIndex 1.
        If dtaBuscarBeneficio.CurrentCell.ColumnIndex = 4 Then
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                ' Remove an existing event-handler, if present, to avoid 
                ' adding multiple handlers when the editing control is reused.
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)

                ' Add the event handler. 
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Dependiendo de la seleccion del usuario, el sistema realiza diferentes acciones.
    ''' -->La opcion ver: consulta un beneficio.
    ''' -->La opcion Editar: muestra un popup, que permite editar un beneficio.
    ''' -->La opcion Eliminar: muestra una alerta que permite eliminar un beneficio.
    ''' </summary>
    ''' <author>Mathias Muller</author>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim combo As ComboBox = CType(sender, ComboBox)

        If combo.SelectedItem = "Ver" Then

            verBeneficios()

        ElseIf combo.SelectedItem = "Editar" Then

            editarBeneficios()

        ElseIf combo.SelectedItem = "Eliminar" Then

            eliminarBeneficios()

        End If

    End Sub

    '//////////////////////////////////////////////////////////////////////////////////////////
    'El ASIGNAR AHORA LO HACE MARIA, NO VA AQUI EN BENEFICIOS!!!!

    'Private Sub btnAsignar_Click(sender As Object, e As EventArgs) Handles btnAsignar.Click

    '    Dim uCtrlAsignarBeneficios As New uCtrlAsignarBeneficios()

    '    frmPrincipal.Controls.Add(uCtrlAsignarBeneficios)
    '    uCtrlAsignarBeneficios.BringToFront()
    '    uCtrlAsignarBeneficios.Show()
    '    uCtrlAsignarBeneficios.Location = New Point(250, 50)

    'End Sub
    '//////////////////////////////////////////////////////////////////////////////////////////

    ''' <summary>
    ''' Muestra un beneficio en un data gird
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub verBeneficios()

        Dim nombre As String = dtaBuscarBeneficio.CurrentRow.Cells(1).Value
        dtaBuscarBeneficio.Rows.Clear()
        Dim objBeneficio As Beneficio = New Beneficio
        objBeneficio = objGestorBeneficio.buscarPorNombre(nombre)


        dtaBuscarBeneficio.Rows.Add(objBeneficio.Id, objBeneficio.Nombre, objBeneficio.Porcentaje, objBeneficio.Aplicacion)
        dtaBuscarBeneficio.Columns("dtaId").Visible = False
        dtaBuscarBeneficio.Columns("dtaOpciones").Visible = False
        dtaBuscarBeneficio.Columns("dtaAplicabilidad").Visible = True

    End Sub

    ''' <summary>
    ''' Edita un beneficio
    ''' </summary>
    ''' <author>Mathias Muller</author>
    ''' <remarks></remarks>
    Private Sub editarBeneficios()


        Dim id As Integer = dtaBuscarBeneficio.CurrentRow.Cells(0).Value
        Dim nombre As String = dtaBuscarBeneficio.CurrentRow.Cells(1).Value
        Dim porcentaje As Double = dtaBuscarBeneficio.CurrentRow.Cells(2).Value
        Dim aplicacion As String = dtaBuscarBeneficio.CurrentRow.Cells(3).Value



        Dim uCtrlModificarBeneficio As New uCtrlModificarBeneficio


        FrmIniciarSesion.principal.Controls.Add(uCtrlModificarBeneficio)
        uCtrlModificarBeneficio.getFrmBuscar(Me)
        uCtrlModificarBeneficio.recieveData(id, nombre, porcentaje, aplicacion)
        uCtrlModificarBeneficio.BringToFront()
        uCtrlModificarBeneficio.Show()
        uCtrlModificarBeneficio.Location = New Point(290, 48)

    End Sub


    ''' <summary>
    ''' Elimina un beneficio
    ''' </summary>
    ''' <author>Mathias Muller</author>
    ''' <remarks></remarks>

    Private Sub eliminarBeneficios()


        Dim id As Integer = dtaBuscarBeneficio.CurrentRow.Cells(0).Value
        Dim nombre As String = dtaBuscarBeneficio.CurrentRow.Cells(1).Value
        Dim porcentaje As Double = dtaBuscarBeneficio.CurrentRow.Cells(2).Value
        Dim aplicacion As String = dtaBuscarBeneficio.CurrentRow.Cells(3).Value

        Dim uCtrlEliminarBeneficio As New uCtrlEliminarBeneficio

        FrmIniciarSesion.principal.Controls.Add(uCtrlEliminarBeneficio)
        uCtrlEliminarBeneficio.getUCtrlInstance(Me)
        uCtrlEliminarBeneficio.lblEliminar.Text = "¿Esta seguro que desea eliminar el beneficio?"
        uCtrlEliminarBeneficio.recibirInfo(id, nombre, porcentaje, aplicacion)
        uCtrlEliminarBeneficio.BringToFront()
        uCtrlEliminarBeneficio.Show()
        uCtrlEliminarBeneficio.Location = New Point(290, 48)

        dtaBuscarBeneficio.Rows.Clear()
        listarBeneficios()

    End Sub


    ''' <summary>
    ''' Busca un beneficio dependiendo del valor del parametro
    ''' Si el parametro es NULL, entonces la lista se referesca nada mas
    ''' </summary>
    ''' <author>Mathias Muller</author>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged

        Dim parametro As String = txtBuscar.Text

        Try

            Dim beneficio As Beneficio = objGestorBeneficio.buscarPorNombre(parametro)

            dtaBuscarBeneficio.Rows.Clear()
            dtaBuscarBeneficio.Rows.Add(1)
            dtaBuscarBeneficio.Rows(0).Cells(0).Value = beneficio.Id
            dtaBuscarBeneficio.Rows(0).Cells(1).Value = beneficio.Nombre
            dtaBuscarBeneficio.Rows(0).Cells(2).Value = beneficio.Porcentaje
            dtaBuscarBeneficio.Rows(0).Cells(3).Value = beneficio.Aplicacion
            dtaBuscarBeneficio.Columns("dtaAplicabilidad").Visible = False
            dtaBuscarBeneficio.Columns("dtaId").Visible = False



        Catch

            dtaBuscarBeneficio.Rows.Clear()
            listarBeneficios()
        End Try
    End Sub
End Class
