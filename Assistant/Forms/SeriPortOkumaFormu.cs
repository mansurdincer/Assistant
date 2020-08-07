using System;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Assistant.Forms
{
    public partial class SeriPortOkumaFormu : XtraForm
    {
        private string inputData = string.Empty;
        public string StokAd;
        public decimal DataValue;
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control:
        private delegate void SetTextCallback(decimal text);

        public SeriPortOkumaFormu()
        {
            InitializeComponent();
        }
        private void frmSerialPort_Load(object sender, EventArgs e)
        {
            lblStokAd.Text = StokAd;
            PortlariListele();
            btKaydet.Select();
        }
        private void PortlariListele()
        {
            // Nice methods to browse all available ports:
            var ports = SerialPort.GetPortNames();

            cmbComSelect.Items.Clear();

            // Add all port names to the combo box:
            foreach (var p in ports.Where(p => p != "COM1"))
            {
                cmbComSelect.Items.Add(p);
            }
            if (cmbComSelect.Items.Count > 0)
            {
                cmbComSelect.SelectedIndex = 0;
            }
        }


        private void cmbComSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (port.IsOpen) port.Close();
            port.PortName = cmbComSelect.SelectedItem.ToString();
            stsStatus.Text = $"{port.PortName} {port.BaudRate} / {port.DataBits} - {port.Parity} - {port.StopBits}";//": 9600,8N1";

            // try to open the selected port:
            try
            {
                port.Open();
            }
            // give a message, if the port is not available:
            catch
            {
                MessageBox.Show($"Seri port {port.PortName} açýlamýyor!", @"Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbComSelect.SelectedText = "";
                stsStatus.Text = @"Seri port seçin!";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (port.IsOpen) port.WriteLine(txtOut.Text);
            else MessageBox.Show(@"Seri port kapalý!", @"Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtOut.Clear();
        }

        private static decimal ConvertToDecimal(String input)
        {
            var divider = input.Contains("kg") ? 1 : (decimal) 0.001;

            // Replace everything that is no a digit.
            String inputCleaned = Regex.Replace(input, @"[^\d.]", "").Replace(".", ",");

            decimal value;

            // Tries to parse the int, returns false on failure.
            if (decimal.TryParse(inputCleaned, out value))
            {
                // The result from parsing can be safely returned.
                return (value * divider);
            }

            return 0; // Or any other default value.
        }

        private void port_DataReceived_1(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                inputData = port.ReadLine();


                if (inputData != String.Empty && inputData.Length > 10)
                {
                    SetText(ConvertToDecimal(inputData));
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(" Data "+exception.Message);
            }

        }

        private void SetText(decimal value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (nudInput.InvokeRequired)
            {
                SetTextCallback d = SetText;
                Invoke(d, value);
            }
            else nudInput.Value = value;

        }

        private void frmSerialPort_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (!port.IsOpen) return;
            //e.Cancel = true; //cancel the fom closing
            //DataValue = Convert.ToDecimal(nudInput.Value);
            //var closeDown = new Thread(CloseSerialOnExit); //close port in new thread to avoid hang

            //closeDown.Start(); //close port in new thread to avoid hang
        }

        private void CloseSerialOnExit()
        {
            try
            {
                port.Close(); //close the serial port
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Hata:" + ex.Message); //catch any serial port closing error messages
            }
            //Invoke(new EventHandler(NowClose)); //now close back in the main thread

        }

        private void btKapat_Click(object sender, EventArgs e)
        {
            if (!port.IsOpen) return;
            //e.Cancel = true; //cancel the fom closing
            DataValue = Convert.ToDecimal(nudInput.Value);
            var closeDown = new Thread(CloseSerialOnExit); //close port in new thread to avoid hang

            closeDown.Start(); //close port in new thread to avoid hang
        }

        private void btIptal_Click(object sender, EventArgs e)
        {
            if (!port.IsOpen) return;
            //e.Cancel = true; //cancel the fom closing
            DataValue = 0;
            var closeDown = new Thread(CloseSerialOnExit); //close port in new thread to avoid hang

            closeDown.Start(); //close port in new thread to avoid hang
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            PortlariListele();
        }

    }
}