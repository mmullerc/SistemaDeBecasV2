﻿Public Class uCtrlReporteRegistroAcciones

    
    Private Sub uCtrlReporteRegistroAcciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Tb_BitacoraAccionTableAdapter.Fill(Me.DB_Proyecto2DataSetConsultarRegistroAcciones.Tb_BitacoraAccion)

        Me.ReportViewer1.RefreshReport()
    End Sub
End Class
