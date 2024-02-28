using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace combine
{

    public partial class Form1 : Form
    {
        public class MyData
        {
            public string s1 { get; set; } = ""; public string s2 { get; set; } = ""; public string s3 { get; set; } = "";
            public string s4 { get; set; } = ""; public string s5 { get; set; } = ""; public string s6 { get; set; } = "";
            public string s7 { get; set; } = ""; public string s8 { get; set; } = ""; public string s9 { get; set; } = "";
            public string s10 { get; set; } = ""; public string s11 { get; set; } = ""; public string s12 { get; set; } = "";
            public string s13 { get; set; } = ""; public string s14 { get; set; } = ""; public string s15 { get; set; } = "";
            public string s16 { get; set; } = ""; public string s17 { get; set; } = ""; public string s18 { get; set; } = "";
            public string s19 { get; set; } = ""; public string s20 { get; set; } = ""; public string s21 { get; set; } = "";
            public string s22 { get; set; } = ""; public string s23 { get; set; } = ""; public string s24 { get; set; } = "";
            public string s25 { get; set; } = ""; public string s26 { get; set; } = ""; public string s27 { get; set; } = "";
            public string s28 { get; set; } = ""; public string s29 { get; set; } = ""; public string s30 { get; set; } = "";
            public string s31 { get; set; } = ""; public string s32 { get; set; } = ""; public string s33 { get; set; } = "";
            public string s34 { get; set; } = ""; public string s35 { get; set; } = ""; public string s36 { get; set; } = "";
            public string s37 { get; set; } = ""; public string s38 { get; set; } = ""; public string s39 { get; set; } = "";
            public string s40 { get; set; } = ""; public string s41 { get; set; } = ""; public string s42 { get; set; } = "";
            public string s43 { get; set; } = ""; public string s44 { get; set; } = ""; public string s45 { get; set; } = "";
            public string s46 { get; set; } = ""; public string s47 { get; set; } = ""; public string s48 { get; set; } = "";
            public string s49 { get; set; } = ""; public string s50 { get; set; } = ""; public string s51 { get; set; } = "";
            public string s52 { get; set; } = ""; public string s53 { get; set; } = ""; public string s54 { get; set; } = "";
            public string s55 { get; set; } = ""; public string s56 { get; set; } = ""; public string s57 { get; set; } = "";
            public string s58 { get; set; } = "";
            public MyData() { }
            public MyData(params string[] values)
            {
                for (int i = 0; i < values.Length && i < 58; i++)
                {
                    GetType().GetProperty($"s{i + 1}")?.SetValue(this, values[i]);
                }
            }
        }
        private List<MyData> dataList = new List<MyData>();
        public Form1()
        {
            InitializeComponent();
            String[] Baudrate = { "115200", "200000" };
            cboBaud.Items.AddRange(Baudrate);
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboPort.DataSource = SerialPort.GetPortNames();
            cboBaud.Text = "115200";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cboPort.Text;
                serialPort1.BaudRate = int.Parse(cboBaud.Text);
                serialPort1.Open();
                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                button1.Enabled = true;
                button2.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine(serialBox.Text);
        }

        private string hex2binary(string hexvalue)
        {
            string binaryval = "";
            binaryval = Convert.ToString(Convert.ToInt32(hexvalue, 16), 2);
            return binaryval;
        }
        private MyData ParseString(string input)
        {
            // Use regular expression to match the pattern "number/number/OK"
            Match match = Regex.Match(input.Trim(), @"^(\d+)/(\d+)/(\w+)$");
            Match match3 = Regex.Match(input.Trim(), @"^(\d+)/(\d+)/([0-9a-fA-F]+):(\d+).(\d+):(\d+).(\d+):(\d+).(\d+)/([0-9a-fA-F]+):(\d+).(\d+):(\d+).(\d+):(\d+).(\d+)/([0-9a-fA-F]+):(\d+).(\d+):(\d+).(\d+):(\d+).(\d+)/([0-9a-fA-F]+):(\d+).(\d+):(\d+).(\d+):(\d+).(\d+)/([0-9a-fA-F]+):(\d+).(\d+):(\d+).(\d+):(\d+).(\d+)/([0-9a-fA-F]+):(\d+).(\d+):(\d+).(\d+):(\d+).(\d+)/([0-9a-fA-F]+):(\d+).(\d+):(\d+).(\d+):(\d+).(\d+)/([0-9a-fA-F]+):(\d+).(\d+):(\d+).(\d+):(\d+).(\d+)$");

            if (match.Success)
            {
                // Extract the individual components
                string a = match.Groups[1].Value;
                string b = match.Groups[2].Value;
                string c = match.Groups[3].Value;

                // Return the extracted values
                return new MyData(a, b, c);
            }
            if (match3.Success)
            {
                string[] n = new string[58];
                for (int i = 0; i < 58; i++)
                {
                    n[i] = match3.Groups[i + 1].Value;
                }

                return new MyData(n);

            }

            // Return null if the input does not match the expected pattern
            return null;
        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox3.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < dataList.Count)
            {
                MyData selectedData = dataList[selectedIndex];
                string bin = "";
                bin = hex2binary(selectedData.s3);
                string[] myStringArray = bin.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                emc_1.Text = myStringArray[0];
                s1_1.Text = myStringArray[1];
                s2_1.Text = myStringArray[2];
                s3_1.Text = myStringArray[3];
                s4_1.Text = myStringArray[4];
                s5_1.Text = myStringArray[5];
                s6_1.Text = myStringArray[6];
                s7_1.Text = myStringArray[7];
                s8_1.Text = myStringArray[8];
                s9_1.Text = myStringArray[9];
                s10_1.Text = myStringArray[10];
                s11_1.Text = myStringArray[11];
                as1_1.Text = selectedData.s4 + "." + selectedData.s5;
                as2_1.Text = selectedData.s6 + "." + selectedData.s7;
                as3_1.Text = selectedData.s8 + "." + selectedData.s9;
                string bin1 = "";
                bin1 = hex2binary(selectedData.s10);
                string[] myStringArray1 = bin1.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                emc_2.Text = myStringArray1[0];
                s1_2.Text = myStringArray1[1];
                s2_2.Text = myStringArray1[2];
                s3_2.Text = myStringArray1[3];
                s4_2.Text = myStringArray1[4];
                s5_2.Text = myStringArray1[5];
                s6_2.Text = myStringArray1[6];
                s7_2.Text = myStringArray1[7];
                s8_2.Text = myStringArray1[8];
                s9_2.Text = myStringArray1[9];
                s10_2.Text = myStringArray1[10];
                s11_2.Text = myStringArray1[11];
                as1_2.Text = selectedData.s11 + "." + selectedData.s12;
                as2_2.Text = selectedData.s13 + "." + selectedData.s14;
                as3_2.Text = selectedData.s15 + "." + selectedData.s16;
                string bin2 = "";
                bin2 = hex2binary(selectedData.s17);
                string[] myStringArray2 = bin2.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                emc_3.Text = myStringArray2[0];
                s1_3.Text = myStringArray2[1];
                s2_3.Text = myStringArray2[2];
                s3_3.Text = myStringArray2[3];
                s4_3.Text = myStringArray2[4];
                s5_3.Text = myStringArray2[5];
                s6_3.Text = myStringArray2[6];
                s7_3.Text = myStringArray2[7];
                s8_3.Text = myStringArray2[8];
                s9_3.Text = myStringArray2[9];
                s10_3.Text = myStringArray2[10];
                s11_3.Text = myStringArray2[11];
                as1_3.Text = selectedData.s18 + "." + selectedData.s19;
                as2_3.Text = selectedData.s20 + "." + selectedData.s21;
                as3_3.Text = selectedData.s22 + "." + selectedData.s23;
                string bin3 = "";
                bin3 = hex2binary(selectedData.s24);
                string[] myStringArray3 = bin3.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                emc_4.Text = myStringArray3[0];
                s1_4.Text = myStringArray3[1];
                s2_4.Text = myStringArray3[2];
                s3_4.Text = myStringArray3[3];
                s4_4.Text = myStringArray3[4];
                s5_4.Text = myStringArray3[5];
                s6_4.Text = myStringArray3[6];
                s7_4.Text = myStringArray3[7];
                s8_4.Text = myStringArray3[8];
                s9_4.Text = myStringArray3[9];
                s10_4.Text = myStringArray3[10];
                s11_4.Text = myStringArray3[11];
                as1_4.Text = selectedData.s25 + "." + selectedData.s26;
                as2_4.Text = selectedData.s27 + "." + selectedData.s28;
                as3_4.Text = selectedData.s29 + "." + selectedData.s30;
                string bin4 = "";
                bin4 = hex2binary(selectedData.s31);
                string[] myStringArray4 = bin4.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                emc_5.Text = myStringArray4[0];
                s1_5.Text = myStringArray4[1];
                s2_5.Text = myStringArray4[2];
                s3_5.Text = myStringArray4[3];
                s4_5.Text = myStringArray4[4];
                s5_5.Text = myStringArray4[5];
                s6_5.Text = myStringArray4[6];
                s7_5.Text = myStringArray4[7];
                s8_5.Text = myStringArray4[8];
                s9_5.Text = myStringArray4[9];
                s10_5.Text = myStringArray4[10];
                s11_5.Text = myStringArray4[11];
                as1_5.Text = selectedData.s32 + "." + selectedData.s33;
                as2_5.Text = selectedData.s34 + "." + selectedData.s35;
                as3_5.Text = selectedData.s36 + "." + selectedData.s37;
                string bin5 = "";
                bin5 = hex2binary(selectedData.s38);
                string[] myStringArray5 = bin5.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                emc_6.Text = myStringArray5[0];
                s1_6.Text = myStringArray5[1];
                s2_6.Text = myStringArray5[2];
                s3_6.Text = myStringArray5[3];
                s4_6.Text = myStringArray5[4];
                s5_6.Text = myStringArray5[5];
                s6_6.Text = myStringArray5[6];
                s7_6.Text = myStringArray5[7];
                s8_6.Text = myStringArray5[8];
                s9_6.Text = myStringArray5[9];
                s10_6.Text = myStringArray5[10];
                s11_6.Text = myStringArray5[11];
                as1_6.Text = selectedData.s39 + "." + selectedData.s40;
                as2_6.Text = selectedData.s41 + "." + selectedData.s42;
                as3_6.Text = selectedData.s43 + "." + selectedData.s44;
                string bin6 = "";
                bin6 = hex2binary(selectedData.s45);
                string[] myStringArray6 = bin6.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                emc_7.Text = myStringArray6[0];
                s1_7.Text = myStringArray6[1];
                s2_7.Text = myStringArray6[2];
                s3_7.Text = myStringArray6[3];
                s4_7.Text = myStringArray6[4];
                s5_7.Text = myStringArray6[5];
                s6_7.Text = myStringArray6[6];
                s7_7.Text = myStringArray6[7];
                s8_7.Text = myStringArray6[8];
                s9_7.Text = myStringArray6[9];
                s10_7.Text = myStringArray6[10];
                s11_7.Text = myStringArray6[11];
                as1_7.Text = selectedData.s46 + "." + selectedData.s47;
                as2_7.Text = selectedData.s48 + "." + selectedData.s49;
                as3_7.Text = selectedData.s50 + "." + selectedData.s51;
                string bin7 = "";
                bin7 = hex2binary(selectedData.s52);
                string[] myStringArray7 = bin7.Select(x => x.ToString()).ToArray();
                emc_8.Text = myStringArray7[0];
                s1_8.Text = myStringArray7[1];
                s2_8.Text = myStringArray7[2];
                s3_8.Text = myStringArray7[3];
                s4_8.Text = myStringArray7[4];
                s5_8.Text = myStringArray7[5];
                s6_8.Text = myStringArray7[6];
                s7_8.Text = myStringArray7[7];
                s8_8.Text = myStringArray7[8];
                s9_8.Text = myStringArray7[9];
                s10_8.Text = myStringArray7[10];
                s11_8.Text = myStringArray7[11];
                as1_8.Text = selectedData.s53 + "." + selectedData.s54;
                as2_8.Text = selectedData.s55 + "." + selectedData.s56;
                as3_8.Text = selectedData.s57 + "." + selectedData.s58;
            }    
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort1.ReadLine();
            listBox1.Items.Add(data);
            MyData parsedValues = ParseString(data);

            if (parsedValues != null)
            {
                listBox3.Items.Add($"{parsedValues.s1}/{parsedValues.s2}/{parsedValues.s3}:{parsedValues.s4}" +
                $".{parsedValues.s5}:{parsedValues.s6}.{parsedValues.s7}:{parsedValues.s8}.{parsedValues.s9}" +
                $"/{parsedValues.s10}:{parsedValues.s11}.{parsedValues.s12}:{parsedValues.s13}.{parsedValues.s14}" +
                $":{parsedValues.s15}.{parsedValues.s16}/{parsedValues.s17}:{parsedValues.s18}.{parsedValues.s19}" +
                $":{parsedValues.s20}.{parsedValues.s21}:{parsedValues.s22}.{parsedValues.s23}/{parsedValues.s24}" +
                $":{parsedValues.s25}.{parsedValues.s26}:{parsedValues.s27}.{parsedValues.s28}:{parsedValues.s29}" +
                $".{parsedValues.s30}/{parsedValues.s31}:{parsedValues.s32}.{parsedValues.s33}:{parsedValues.s34}.{parsedValues.s35}" +
                $":{parsedValues.s36}.{parsedValues.s37}/{parsedValues.s38}:{parsedValues.s39}.{parsedValues.s40}:{parsedValues.s41}" +
                $".{parsedValues.s42}:{parsedValues.s43}.{parsedValues.s44}/{parsedValues.s45}:{parsedValues.s46}" +
                $".{parsedValues.s47}:{parsedValues.s48}.{parsedValues.s49}:{parsedValues.s50}.{parsedValues.s51}" +
                $"/{parsedValues.s52}:{parsedValues.s53}.{parsedValues.s54}:{parsedValues.s55}.{parsedValues.s56}" +
                $":{parsedValues.s57}.{parsedValues.s58}");

                dataList.Add(parsedValues);

                if (parsedValues.s1 == "2" &&
                    int.TryParse(parsedValues.s2, out int parseds2) && parseds2 >= 1 && parseds2 <= 50 &&
                    int.TryParse(parsedValues.s3, System.Globalization.NumberStyles.HexNumber, null, out _) &&
                    int.TryParse(parsedValues.s4, out int parseds4) && int.TryParse(parsedValues.s5, out int parseds5)
                    && int.TryParse(parsedValues.s6, out int parseds6) && int.TryParse(parsedValues.s7, out int parseds7)
                    && int.TryParse(parsedValues.s8, out int parseds8) && int.TryParse(parsedValues.s9, out int parseds9) &&
                    int.TryParse(parsedValues.s10, System.Globalization.NumberStyles.HexNumber, null, out _) &&
                    int.TryParse(parsedValues.s11, out int parseds11) && int.TryParse(parsedValues.s12, out int parseds12)
                    && int.TryParse(parsedValues.s13, out int parseds13) && int.TryParse(parsedValues.s14, out int parseds14)
                    && int.TryParse(parsedValues.s15, out int parseds15) && int.TryParse(parsedValues.s16, out int parseds16)
                    && int.TryParse(parsedValues.s17, System.Globalization.NumberStyles.HexNumber, null, out _) &&
                    int.TryParse(parsedValues.s18, out int parseds18) && int.TryParse(parsedValues.s19, out int parseds19)
                    && int.TryParse(parsedValues.s20, out int parseds20) && int.TryParse(parsedValues.s21, out int parseds21)
                    && int.TryParse(parsedValues.s22, out int parseds22) && int.TryParse(parsedValues.s23, out int parseds23)
                    && int.TryParse(parsedValues.s24, System.Globalization.NumberStyles.HexNumber, null, out _) &&
                    int.TryParse(parsedValues.s25, out int parseds25) && int.TryParse(parsedValues.s26, out int parseds26)
                    && int.TryParse(parsedValues.s27, out int parseds27) && int.TryParse(parsedValues.s28, out int parseds28)
                    && int.TryParse(parsedValues.s29, out int parseds29) && int.TryParse(parsedValues.s30, out int parseds30)
                    && int.TryParse(parsedValues.s31, System.Globalization.NumberStyles.HexNumber, null, out _) &&
                    int.TryParse(parsedValues.s32, out int parseds32) && int.TryParse(parsedValues.s33, out int parseds33)
                    && int.TryParse(parsedValues.s34, out int parseds34) && int.TryParse(parsedValues.s35, out int parseds35)
                    && int.TryParse(parsedValues.s36, out int parseds36) && int.TryParse(parsedValues.s37, out int parseds37)
                    && int.TryParse(parsedValues.s38, System.Globalization.NumberStyles.HexNumber, null, out _) &&
                    int.TryParse(parsedValues.s39, out int parseds39) && int.TryParse(parsedValues.s40, out int parseds40)
                    && int.TryParse(parsedValues.s41, out int parseds41) && int.TryParse(parsedValues.s42, out int parseds42)
                    && int.TryParse(parsedValues.s43, out int parseds43) && int.TryParse(parsedValues.s44, out int parseds44)
                    && int.TryParse(parsedValues.s45, System.Globalization.NumberStyles.HexNumber, null, out _) &&
                    int.TryParse(parsedValues.s46, out int parseds46) && int.TryParse(parsedValues.s47, out int parseds47)
                    && int.TryParse(parsedValues.s48, out int parseds48) && int.TryParse(parsedValues.s49, out int parseds49)
                    && int.TryParse(parsedValues.s50, out int parseds50) && int.TryParse(parsedValues.s51, out int parseds51)
                    && int.TryParse(parsedValues.s52, System.Globalization.NumberStyles.HexNumber, null, out _) &&
                    int.TryParse(parsedValues.s53, out int parseds53) && int.TryParse(parsedValues.s54, out int parseds54)
                    && int.TryParse(parsedValues.s55, out int parseds55) && int.TryParse(parsedValues.s56, out int parseds56)
                    && int.TryParse(parsedValues.s57, out int parseds57) && int.TryParse(parsedValues.s58, out int parseds58))
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        listBox2.Items.Add($"Device online:{parsedValues.s2}");
                        string bin = "";
                        bin = hex2binary(parsedValues.s3);
                        string[] myStringArray = bin.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                        emc_1.Text = myStringArray[0];
                        s1_1.Text = myStringArray[1];
                        s2_1.Text = myStringArray[2];
                        s3_1.Text = myStringArray[3];
                        s4_1.Text = myStringArray[4];
                        s5_1.Text = myStringArray[5];
                        s6_1.Text = myStringArray[6];
                        s7_1.Text = myStringArray[7];
                        s8_1.Text = myStringArray[8];
                        s9_1.Text = myStringArray[9];
                        s10_1.Text = myStringArray[10];
                        s11_1.Text = myStringArray[11];
                        as1_1.Text = parsedValues.s4 + "." + parsedValues.s5;
                        as2_1.Text = parsedValues.s6 + "." + parsedValues.s7;
                        as3_1.Text = parsedValues.s8 + "." + parsedValues.s9;
                        string bin1 = "";
                        bin1 = hex2binary(parsedValues.s10);
                        string[] myStringArray1 = bin1.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                        emc_2.Text = myStringArray1[0];
                        s1_2.Text = myStringArray1[1];
                        s2_2.Text = myStringArray1[2];
                        s3_2.Text = myStringArray1[3];
                        s4_2.Text = myStringArray1[4];
                        s5_2.Text = myStringArray1[5];
                        s6_2.Text = myStringArray1[6];
                        s7_2.Text = myStringArray1[7];
                        s8_2.Text = myStringArray1[8];
                        s9_2.Text = myStringArray1[9];
                        s10_2.Text = myStringArray1[10];
                        s11_2.Text = myStringArray1[11];
                        as1_2.Text = parsedValues.s11 + "." + parsedValues.s12;
                        as2_2.Text = parsedValues.s13 + "." + parsedValues.s14;
                        as3_2.Text = parsedValues.s15 + "." + parsedValues.s16;
                        string bin2 = "";
                        bin2 = hex2binary(parsedValues.s17);
                        string[] myStringArray2 = bin2.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                        emc_3.Text = myStringArray2[0];
                        s1_3.Text = myStringArray2[1];
                        s2_3.Text = myStringArray2[2];
                        s3_3.Text = myStringArray2[3];
                        s4_3.Text = myStringArray2[4];
                        s5_3.Text = myStringArray2[5];
                        s6_3.Text = myStringArray2[6];
                        s7_3.Text = myStringArray2[7];
                        s8_3.Text = myStringArray2[8];
                        s9_3.Text = myStringArray2[9];
                        s10_3.Text = myStringArray2[10];
                        s11_3.Text = myStringArray2[11];
                        as1_3.Text = parsedValues.s18 + "." + parsedValues.s19;
                        as2_3.Text = parsedValues.s20 + "." + parsedValues.s21;
                        as3_3.Text = parsedValues.s22 + "." + parsedValues.s23;
                        string bin3 = "";
                        bin3 = hex2binary(parsedValues.s24);
                        string[] myStringArray3 = bin3.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                        emc_4.Text = myStringArray3[0];
                        s1_4.Text = myStringArray3[1];
                        s2_4.Text = myStringArray3[2];
                        s3_4.Text = myStringArray3[3];
                        s4_4.Text = myStringArray3[4];
                        s5_4.Text = myStringArray3[5];
                        s6_4.Text = myStringArray3[6];
                        s7_4.Text = myStringArray3[7];
                        s8_4.Text = myStringArray3[8];
                        s9_4.Text = myStringArray3[9];
                        s10_4.Text = myStringArray3[10];
                        s11_4.Text = myStringArray3[11];
                        as1_4.Text = parsedValues.s25 + "." + parsedValues.s26;
                        as2_4.Text = parsedValues.s27 + "." + parsedValues.s28;
                        as3_4.Text = parsedValues.s29 + "." + parsedValues.s30;
                        string bin4 = "";
                        bin4 = hex2binary(parsedValues.s31);
                        string[] myStringArray4 = bin4.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                        emc_5.Text = myStringArray4[0];
                        s1_5.Text = myStringArray4[1];
                        s2_5.Text = myStringArray4[2];
                        s3_5.Text = myStringArray4[3];
                        s4_5.Text = myStringArray4[4];
                        s5_5.Text = myStringArray4[5];
                        s6_5.Text = myStringArray4[6];
                        s7_5.Text = myStringArray4[7];
                        s8_5.Text = myStringArray4[8];
                        s9_5.Text = myStringArray4[9];
                        s10_5.Text = myStringArray4[10];
                        s11_5.Text = myStringArray4[11];
                        as1_5.Text = parsedValues.s32 + "." + parsedValues.s33;
                        as2_5.Text = parsedValues.s34 + "." + parsedValues.s35;
                        as3_5.Text = parsedValues.s36 + "." + parsedValues.s37;
                        string bin5 = "";
                        bin5 = hex2binary(parsedValues.s38);
                        string[] myStringArray5 = bin5.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                        emc_6.Text = myStringArray5[0];
                        s1_6.Text = myStringArray5[1];
                        s2_6.Text = myStringArray5[2];
                        s3_6.Text = myStringArray5[3];
                        s4_6.Text = myStringArray5[4];
                        s5_6.Text = myStringArray5[5];
                        s6_6.Text = myStringArray5[6];
                        s7_6.Text = myStringArray5[7];
                        s8_6.Text = myStringArray5[8];
                        s9_6.Text = myStringArray5[9];
                        s10_6.Text = myStringArray5[10];
                        s11_6.Text = myStringArray5[11];
                        as1_6.Text = parsedValues.s39 + "." + parsedValues.s40;
                        as2_6.Text = parsedValues.s41 + "." + parsedValues.s42;
                        as3_6.Text = parsedValues.s43 + "." + parsedValues.s44;
                        string bin6 = "";
                        bin6 = hex2binary(parsedValues.s45);
                        string[] myStringArray6 = bin6.PadLeft(12, '0').Select(x => x.ToString()).ToArray();
                        emc_7.Text = myStringArray6[0];
                        s1_7.Text = myStringArray6[1];
                        s2_7.Text = myStringArray6[2];
                        s3_7.Text = myStringArray6[3];
                        s4_7.Text = myStringArray6[4];
                        s5_7.Text = myStringArray6[5];
                        s6_7.Text = myStringArray6[6];
                        s7_7.Text = myStringArray6[7];
                        s8_7.Text = myStringArray6[8];
                        s9_7.Text = myStringArray6[9];
                        s10_7.Text = myStringArray6[10];
                        s11_7.Text = myStringArray6[11];
                        as1_7.Text = parsedValues.s46 + "." + parsedValues.s47;
                        as2_7.Text = parsedValues.s48 + "." + parsedValues.s49;
                        as3_7.Text = parsedValues.s50 + "." + parsedValues.s51;
                        string bin7 = "";
                        bin7 = hex2binary(parsedValues.s52);
                        string[] myStringArray7 = bin7.Select(x => x.ToString()).ToArray();
                        emc_8.Text = myStringArray7[0];
                        s1_8.Text = myStringArray7[1];
                        s2_8.Text = myStringArray7[2];
                        s3_8.Text = myStringArray7[3];
                        s4_8.Text = myStringArray7[4];
                        s5_8.Text = myStringArray7[5];
                        s6_8.Text = myStringArray7[6];
                        s7_8.Text = myStringArray7[7];
                        s8_8.Text = myStringArray7[8];
                        s9_8.Text = myStringArray7[9];
                        s10_8.Text = myStringArray7[10];
                        s11_8.Text = myStringArray7[11];
                        as1_8.Text = parsedValues.s53 + "." + parsedValues.s54;
                        as2_8.Text = parsedValues.s55 + "." + parsedValues.s56;
                        as3_8.Text = parsedValues.s57 + "." + parsedValues.s58;

                    }));
                }


                // Check conditions before displaying in listBox2
                if (parsedValues.s1 == "1" && int.TryParse(parsedValues.s2, out int parsedSecondNumber) && parsedSecondNumber >= 1 && parsedSecondNumber <= 50 && parsedValues.s3 == "OK")
                {

                    // Display the extracted values in listBox2 with the specified conditions
                    Invoke(new MethodInvoker(() =>
                    {
                        listBox4.Items.Add($"Device: {parsedSecondNumber} - Status: {parsedValues.s3}");
                    }));
                }

            }
        }


    }
}
