﻿Public Class uCtrlEliminarBeneficio

    Dim id As Integer
    Dim nombre As String
    Dim porcentaje As Double
    Dim aplicabilidad As String
    Dim uCtrl As uCntrlBuscarBeneficio
    Dim mBlnFormDragging As Boolean


    ''' <summary>
    ''' Recibe una instancia del userControlBuscarBeneficio y la setea.
    ''' </summary>
    ''' <author>Mathias Muller</author>
    ''' <param name="puCtrl">Es una instancia del user control</param>
    ''' <remarks></remarks>
    Public Sub getUCtrlInstance(puCtrl As uCntrlBuscarBeneficio)

        uCtrl = puCtrl

    End Sub

    ''' <summary>
    ''' Recibe la informaciondel beneficio que sera eliminado.
    ''' </summary>
    ''' <author>Mathias Muller</author>
    ''' <param name="pid">Es el id del beneficio</param>
    ''' <param name="pnombre">El nombre del beneficio</param>
    ''' <param name="pporcentaje">El porcentaje del beneficio</param>
    ''' <param name="paplicabilidad">La aplicabilidad del beneficio</param>
    ''' <remarks></remarks>
    Public Sub recibirInfo(ByVal pid As Integer, ByVal pnombre As String, ByVal pporcentaje As Double, ByVal paplicabilidad As String)

        id = pid
        nombre = pnombre
        porcentaje = pporcentaje
        aplicabilidad = paplicabilidad

    End Sub

    ''' <summary>
    ''' Envia los parametros del beneficio al gestor.
    ''' Refresa la el dataGrid con la informacion nueva.
    ''' </summary>
    ''' <author>Mathias Muller</author>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click

        Try

            objGestorBeneficio.eliminarBeneficio(id, nombre, porcentaje, aplicabilidad)
            objGestorBeneficio.guardarCambios()
            Dim Uctrl As uCtrlConfirmacion = New uCtrlConfirmacion
            FrmIniciarSesion.principal.Controls.Add(Uctrl)
            Uctrl.lblConfirmacion.Text = "El beneficio se eliminó correctamente"
            Uctrl.Location = New Point(300, 100)
            Uctrl.BringToFront()
            Uctrl.Show()


        Catch ex As Exception

            Dim UCtrl As UCtrlAlerta = New UCtrlAlerta()

            FrmIniciarSesion.principal.Controls.Add(UCtrl)
            UCtrl.lblAlerta.Text = ex.Message
            UCtrl.Location = New Point(250, 50)
            UCtrl.BringToFront()
            UCtrl.Show()



        End Try

        uCtrl.dtaBuscarBeneficio.Rows.Clear()
        uCtrl.listarBeneficios()

        Me.Dispose()
        Me.Hide()


    End Sub

    ''' <summary>
    ''' Esconde y destruye el popup de eliminar beneficio.
    ''' </summary>
    ''' <author>Mathias Muller</author>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Dispose()
        Me.Hide()
    End Sub

    Private Sub uCtrlEliminarBeneficio_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove

        If mBlnFormDragging = True Then

            Dim position As Point = frmPrincipal.PointToClient(MousePosition)
            Me.Location = New Point(position)

        End If

    End Sub

    Private Sub uCtrlEliminarBeneficio_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp

        mBlnFormDragging = False
        Dim position As Point = frmPrincipal.PointToClient(MousePosition)
        Location = New Point(position)

    End Sub

    Public Sub uCtrlEliminarBeneficio_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown

        mBlnFormDragging = True

    End Sub

End Class
