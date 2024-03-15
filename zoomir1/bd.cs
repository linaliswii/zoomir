using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zoomir1
{
    public partial class bd : Form
    {
        private List<int> selectedRowsIndexes = new List<int>();
        public bd()
        {
            InitializeComponent();
        }

        private void bd_Load(object sender, EventArgs e)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=zoomir";
            dataGridView1.CellClick += dataGridView1_CellClick_1;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT Номер_товара, Фото_товара, Наименование, Описание, Стоимость FROM products";

                    using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(selectQuery, connection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        foreach (DataRow row in dataTable.Rows)
                        {
                            byte[] imageData = (byte[])row["Фото_товара"];
                            Image originalImage = ByteArrayToImage(imageData);
                            int desiredWidth = 100; // Замените на ваше желаемое значение ширины
                            int desiredHeight = 100; // Замените на ваше желаемое значение высоты
                            Image resizedImage = ResizeImage(originalImage, desiredWidth, desiredHeight);

                            row["Фото_товара"] = ImageToByteArray(resizedImage);
                        }
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            dataGridView1.RowTemplate.Height = 100; 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = dataGridView1.RowTemplate.Height;
            }
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(image, 0, 0, width, height);
            }
            return result;
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); 
                return ms.ToArray();
            }
        }

        private void btntalon_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;

            if (selectedRows.Count > 0)
            {
                // Генерируем уникальный номер заказа
                Random random = new Random();
                int orderNumber = random.Next(100000, 999999);

                // Собираем информацию о заказе
                StringBuilder orderComposition = new StringBuilder();
                decimal totalAmount = 0;

                foreach (DataGridViewRow row in selectedRows)
                {
                    string productName = row.Cells["Наименование"].Value.ToString();
                    decimal productPrice = (decimal)row.Cells["Стоимость"].Value; // Приведение типа к decimal

                    // Добавляем информацию о товаре к составу заказа
                    orderComposition.AppendLine($"{productName} - {productPrice:C}");

                    // Суммируем стоимость товаров
                    totalAmount += productPrice;
                }
                InsertOrderData();

            }
            else
            {
                MessageBox.Show("Выберите товары для заказа.");
            }

            talon talon = new talon();
            talon.Show();
            this.Hide();
        }

        private void InsertOrderData()
        {
            string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=zoomir";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                Random random = new Random();
                int orderNumber = random.Next(1000, 9999);
                StringBuilder orderDetails = new StringBuilder();
                decimal totalAmount = 0;

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string productName = row.Cells["Наименование"].Value.ToString();
                    decimal productPrice = Convert.ToDecimal(row.Cells["Стоимость"].Value);
                    orderDetails.AppendLine($"{productName}: {productPrice:C}");
                    totalAmount += productPrice;
                }

                string insertOrderQuery = "INSERT INTO orders (Номер_заказа, Позиции_заказа, Общая_сумма, Дата_заказа, Код_выдачи) " +
                           "VALUES (@Номер_заказа, @Позиции_заказа, @Общая_сумма, CURRENT_DATE, @Код_выдачи)";

                // Генерация 6-значного кода
                string sixDigitCode = GenerateSixDigitCode();

                using (NpgsqlCommand command = new NpgsqlCommand(insertOrderQuery, connection))
                {
                    command.Parameters.AddWithValue("@Номер_заказа", orderNumber);
                    command.Parameters.AddWithValue("@Позиции_заказа", orderDetails.ToString());
                    command.Parameters.AddWithValue("@Общая_сумма", totalAmount);
                    command.Parameters.AddWithValue("@Код_выдачи", sixDigitCode);


                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заказ успешно добавлен!");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении заказа.");
                    }
                }
            }
        }
                
                private string GenerateSixDigitCode()
                {
                    Random random = new Random();
                    return random.Next(100, 999).ToString();
                }

        

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;

                // Если строка уже была выбрана, снимаем выделение
                if (selectedRowsIndexes.Contains(rowIndex))
                {
                    selectedRowsIndexes.Remove(rowIndex);
                }
                else
                {
                    // Добавляем в список выбранных строк
                    selectedRowsIndexes.Add(rowIndex);
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


