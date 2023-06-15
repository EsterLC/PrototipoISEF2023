using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using Capa_Modelo_EF;

namespace Capa_Vista_EF
{
    
    public partial class Facturación : Form
    {
        Conexion conexion = new Conexion();
        public Facturación()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 5; // Establece el número de columnas

            dataGridView1.Columns[0].Name = "Sala"; // Nombre de la primera columna
            dataGridView1.Columns[1].Name = "Fila"; // Nombre de la segunda columna
            dataGridView1.Columns[2].Name = "Columna"; // Nombre de la tercera columna
            dataGridView1.Columns[3].Name = "Cine"; // Nombre de la cuarta columna
            dataGridView1.Columns[4].Name = "Dirección"; // Nombre de la quinta columna

            string comboCliente = "select Nit from `cinevision`.`clientes`";
            string comboReserva = "select idReservaciones from `cinevision`.`reservaciones`";

            OdbcCommand cmd = new OdbcCommand(comboCliente, conexion.conexion());
            OdbcDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBoxCliente.Items.Add(reader.GetString(0));
            }

            OdbcCommand cmdReserva = new OdbcCommand(comboReserva, conexion.conexion());
            OdbcDataReader readerReserva = cmdReserva.ExecuteReader();

            while (readerReserva.Read())
            {
                comboBoxReserva.Items.Add(readerReserva.GetString(0));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nit = comboBoxCliente.SelectedItem.ToString();
            string reserva = comboBoxReserva.SelectedItem.ToString();
            string cliente = "SELECT idClientes FROM clientes WHERE Nit =  " + nit + ";";
            OdbcCommand cmdCliente = new OdbcCommand(cliente, conexion.conexion());
            OdbcDataReader readerCliente = cmdCliente.ExecuteReader();

            while (readerCliente.Read())
            {
                txt_idCliente.Text = readerCliente.GetString(0);
            }
            DateTime fecha = dateTimePicker1.Value;
            txt_fecha.Text = fecha.ToString("yyyy/MM/dd"); // Formato de fecha deseado
            string factura = "INSERT INTO factura_encabezado VALUES (" + txt_id.Text + ",'" + txt_fecha.Text + "','" + txt_estado.Text + "'," + txt_idCliente.Text + ");";
            OdbcCommand cmdInsertar = new OdbcCommand(factura, conexion.conexion());
            cmdInsertar.ExecuteNonQuery();

            string facturaDetalle = "INSERT INTO factura_detalle VALUES (" + txt_id.Text + "," + txt_id.Text + "," + reserva + ",'" + txt_pago.Text + "'," + txt_total.Text + ");";
            OdbcCommand cmdInsertarDetalle = new OdbcCommand(facturaDetalle, conexion.conexion());
            cmdInsertarDetalle.ExecuteNonQuery();
            MessageBox.Show("Dato Insertado");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string reserva = comboBoxReserva.SelectedItem.ToString();
            txt_reserva.Text = reserva;

            string sala = "SELECT Descripción FROM salas, asientos WHERE idSalas = fkSala AND idAsientos = " + txt_reserva.Text + ";";
            OdbcCommand cmdSala = new OdbcCommand(sala, conexion.conexion());
            OdbcDataReader readerSala = cmdSala.ExecuteReader();

            while (readerSala.Read())
            {
                txt_Sala.Text = readerSala.GetString(0);
            }
            string fila = "SELECT asientos.Fila FROM asientos, reservaciones WHERE idAsientos = fkAsientos AND idReservaciones = " + txt_reserva.Text + ";";
            OdbcCommand cmd = new OdbcCommand(fila, conexion.conexion());
            OdbcDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txt_fila.Text = reader.GetString(0);
            }
            string columna = "SELECT asientos.Columna FROM asientos, reservaciones WHERE idAsientos = fkAsientos AND idReservaciones = " + txt_reserva.Text + ";";
            OdbcCommand cmdColumna = new OdbcCommand(columna, conexion.conexion());
            OdbcDataReader readerColumna = cmdColumna.ExecuteReader();

            while (readerColumna.Read())
            {
                txt_columna.Text = readerColumna.GetString(0);
            }
            string total = "SELECT subTotalAsiento FROM reservaciones WHERE idReservaciones = " + txt_reserva.Text + ";";
            OdbcCommand cmdTotal = new OdbcCommand(total, conexion.conexion());
            OdbcDataReader readertotal = cmdTotal.ExecuteReader();

            while (readertotal.Read())
            {
                txt_total.Text = readertotal.GetString(0);
            }

            string cine = "SELECT Nombre FROM cines, salas WHERE fkCine = idCines AND idSalas = "+ txt_reserva.Text + ";";
            OdbcCommand cmdCine = new OdbcCommand(cine, conexion.conexion());
            OdbcDataReader readerCine = cmdCine.ExecuteReader();

            while (readerCine.Read())
            {
                txt_cine.Text = readerCine.GetString(0);
            }

            string dire = "SELECT Dirección FROM cines, salas WHERE fkCine = idCines AND idSalas = " + txt_reserva.Text + ";";
            OdbcCommand cmdDire = new OdbcCommand(dire, conexion.conexion());
            OdbcDataReader readerDire = cmdDire.ExecuteReader();

            while (readerDire.Read())
            {
                txt_direccion.Text = readerDire.GetString(0);
            }

            string dato1 = txt_Sala.Text;
            string dato2 = txt_fila.Text;
            string dato3 = txt_columna.Text;
            string dato4 = txt_cine.Text;
            string dato5 = txt_direccion.Text;
            string[] row1 = { dato1, dato2, dato3, dato4,dato5}; // Valores de la primera fila
            dataGridView1.Rows.Add(row1); // Agrega la primera fila
        }
    }
}
