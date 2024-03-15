using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace zoomir1
{
    public partial class talon : Form
    {
        public talon()
        {
            InitializeComponent();
        }

        private void talon_Load(object sender, EventArgs e)
        {
            
            string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=zoomir";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT id, Номер_заказа, Позиции_заказа, Общая_сумма FROM orders";

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(selectQuery, connection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btntalon_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;

            if (selectedRows.Count > 0)
            {
                Random random = new Random();
                int orderNumber = random.Next(100000, 999999);
                StringBuilder orderComposition = new StringBuilder();
                decimal totalAmount = 0;

                foreach (DataGridViewRow row in selectedRows)
                {
                    string orderDetails = row.Cells["Позиции_заказа"].Value?.ToString() ?? "";
                    decimal orderPrice = 0;
                    decimal.TryParse(row.Cells["Общая_сумма"].Value?.ToString(), out orderPrice);
                    orderComposition.AppendLine($"{orderDetails} - {orderPrice:C}");
                    totalAmount += orderPrice;
                }
                string pickupLocation = comboBoxPickupPoints.SelectedItem?.ToString() ?? "Не выбрано";
                int pickupCode = random.Next(100, 999);
                GeneratePdf(orderNumber.ToString(), orderComposition.ToString(), pickupLocation.ToString(), totalAmount, pickupCode.ToString());
            }
            else
            {
                MessageBox.Show("Выберите заказы для формирования талона.");
            }
        }

        private void GeneratePdf(string orderNumber, string orderComposition, string pickupLocation, decimal totalAmount, string pickupCode)
        {
            string pdfFileName = $"Заказ_{orderNumber}.pdf";
            Document pdfDocument = new Document();
            Page page = pdfDocument.Pages.Add();
            TextFragment textFragment = new TextFragment();
            textFragment.Text = $"Номер заказа: {orderNumber}\n" +
                                $"Позиции заказа: {orderComposition}\n" +
                                $"Сумма заказа: {totalAmount:C}\n" +
                                $"Пункт выдачи: {pickupLocation}\n" +
                                $"Код получения заказа: {(Convert.ToInt32(pickupCode) % 1000):D3}";

            page.Paragraphs.Add(textFragment);
            pdfDocument.Save(pdfFileName);
            Process.Start(pdfFileName);
        }

        private void comboBoxPickupPoints_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
