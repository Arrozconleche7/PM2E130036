using Microsoft.Maui.Controls;
using System;

namespace PM2E130036
{
    public partial class EnviosPage : ContentPage
    {
        public EnviosPage()
        {
            InitializeComponent();
        }

        private void OnCalcularClicked(object sender, EventArgs e)
        {
            // Validar descripción vacía
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                DisplayAlert("Atención", "Debe ingresar la descripción del paquete.", "OK");
                return;
            }

            // Validar peso con TryParse, no vacío y mayor a cero
            if (!double.TryParse(txtPeso.Text, out double peso) || peso <= 0)
            {
                DisplayAlert("Atención", "Debe ingresar un peso válido mayor a cero.", "OK");
                return;
            }

            // Validar selección del Picker
            if (pckZona.SelectedIndex == -1)
            {
                DisplayAlert("Atención", "Debe seleccionar una zona de destino.", "OK");
                return;
            }

            double tarifaBase = 0;
            string nombreZona = "";

            // Asignar tarifa según la zona seleccionada
            switch (pckZona.SelectedIndex)
            {
                case 0: // Zona local
                    tarifaBase = 50.00;
                    nombreZona = "Envio Local";
                    break;
                case 1: // Zona nacional
                    tarifaBase = 85.00;
                    nombreZona = "Envio Nacional";
                    break;
                case 2: // Zona internacional
                    tarifaBase = 150.00;
                    nombreZona = "Envio Internacional";
                    break;
            }

            // Cálculo principal
            double costoTotal = peso * tarifaBase;
            string detalleServicio = $"{peso} kg";

            // Condicional de recargo
            if (chkExpress.IsChecked)
            {
                costoTotal += 100.00;
                detalleServicio += " + servicio express";
            }

            // Mostrar resultados
            lblCostoTotal.Text = $"Costo Total: L {costoTotal:F2}";
            lblDetalle.Text = $"detalle: {nombreZona}\n({detalleServicio})";

            // Cambio dinámico de color
            if (costoTotal < 300)
            {
                brdResultado.BackgroundColor = Color.FromArgb("#DCFCE7");
            }
            else
            {
                brdResultado.BackgroundColor = Color.FromArgb("#E0F2FE");
            }

            brdResultado.IsVisible = true;
        }
    }
}