using System.Windows.Forms;
using System;

namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0; //�w�]�Ա�
            comboBox2.SelectedIndex = 0; //�w�]����|
            radioButton1.Checked = true; //�w�]�p��ƶq

            foreach (Control c in this.Controls) //�Ҧ�textbox�Ҩϥλ��jforeach�M��KeyPress��h
            {
                if (c is TextBox)
                {
                    c.KeyPress += new KeyPressEventHandler(KeyPress);
                }
            }
        }

        private void CheckedChanged(object sender, EventArgs e) //�ֳt��ܡA�ثe�����յP��˨��س��b�o
        {
            domainUpDown1.SelectedIndex = 8;
            if (cygnus.Checked)
            {
                bore.Text = "52.4";   //���|
                length.Text = "57.9"; //��{
                cap.Text = "124.86";  //�e�n
            }
            else if (gryphus.Checked)
            {
                bore.Text = "52";
                length.Text = "58.7";
                cap.Text = "124.66";
            }
            else if (smax.Checked)
            {
                bore.Text = "58";
                length.Text = "58.7";
                cap.Text = "155.09";
            }
            else if (jets.Checked)
            {
                bore.Text = "54";
                length.Text = "54.4";
                cap.Text = "124.59";
            }
            else if (sl.Checked)
            {
                bore.Text = "53";
                length.Text = "56.5";
                cap.Text = "124.65";
            }
            else if (drg.Checked)
            {
                bore.Text = "59";
                length.Text = "57.8";
                cap.Text = "158.02";
            }
            else if (rcs125.Checked)
            {
                bore.Text = "54";
                length.Text = "54.5";
                cap.Text = "124.82";
            }
            else if (rcs150.Checked)
            {
                bore.Text = "59";
                length.Text = "54.5";
                cap.Text = "149.00";
            }
            else if (krv.Checked)
            {
                bore.Text = "62";
                length.Text = "58";
                cap.Text = "175.11";
            }
            TextChanged(sender, e); //Ĳ�oTextChanged�ƥ�
        }

        private void KeyPress(object sender, KeyPressEventArgs e) //����textbox�ȯ��J�Ʀr�B�p���I�Ȥ@��B���ର�}�Y�B�t�ƶȤ@��B�ȭ��}�Y
        {
            TextBox tb = sender as TextBox;

            //Check if the key pressed is a number or a backspace, negative sign or dot
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && tb.Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            // Check if there's already a negative sign or decimal point in the text
            if ((e.KeyChar == '-' && tb.SelectionStart != 0) || (e.KeyChar == '.' && tb.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            // check if the first char is not negative sign
            if (tb.Text.Length == 0 && e.KeyChar == '.')
            {
                e.Handled = true;
            }
        }
        
        private void TextChanged(object sender, EventArgs e) //��Ʀ۰ʱa�J�çY�ɧ�s�A�p��ƶq
        {
            //�N��J��string�ରdouble
            double.TryParse(bore.Text, out double boref);               //���|
            double.TryParse(length.Text, out double lengthf);           //��{
            double.TryParse(cap.Text, out double capf);                 //�e�n
            double.TryParse(newbore.Text, out double newboref);         //�s���|
            double.TryParse(addlength.Text, out double addlengthf);     //�[����{
            double.TryParse(newcap.Text, out double newcapf);           //�s�e�n
            int.TryParse(domainUpDown1.Text, out int domainUpDown1i);   //����

            double math = 0.0007854; //�ƾǤ���
            /*
             * 0.7854 �O�@�ӼƭȡA�Ω�p���W����n���������C
             * ����ӻ��A���O�H�|��p����ܪ���P�v�]�k�^���ȡC
             * ��W����n�������O�GV = �kr^2h
             * �䤤 V �O��n�Ar �O��W�驳���b�|�Ah �O��W�鰪�A�k �O��P�v�`�ơC
             * �b�o�Ӥ������A0.7854 �i�H�Χ@ �k ������ȡA���z�b�w���b�|�M���׮ɥi�H�p���W�骺��n�C
             * ���S�]�����|��{�Ҭ��@�̡A�G�ݰ��H1000���p��Xcc��
             */

            double newborefset = 0; //�s���|(��ܳ���)
            double addlengthfset = 0; //��{(��ܳ���)

            multiplier.Text = (newcapf / capf).ToString("0.00");    //�p�⭿�v
            multiplierCC.Text = (newcapf - capf).ToString("0.00");  //�p��cc��
            percent.Text = ((newcapf - capf) / capf * 100).ToString("0.00"); //�p��ʤ���

            if (comboBox2.SelectedIndex == 0) //��ܬ��|
            {
                label11.Text = "mm";
                newborefset = newboref;
            }
            else if (comboBox2.SelectedIndex == 1) //��ܷe��
            {
                label11.Text = "��";
                newborefset = boref + (newboref * 0.01);
            }

            if (comboBox1.SelectedIndex == 0) //��ܩԱ�
            {
                label12.Text = "��";
                addlengthfset = lengthf + (addlengthf * 0.01);
            }
            else if (comboBox1.SelectedIndex == 1) //����`��{
            {
                label12.Text = "mm";
                addlengthfset = addlengthf;
            }

            double boref2 = boref * boref; //���|����
            double newboref2 = newborefset * newborefset; //�s���|����

            if (newbore.Text != "")  //�ȧ�����Ա�
                newcap.Text = (newboref2 * addlengthfset * math * domainUpDown1i).ToString("0.00");
            else if (addlength.Text != "") //�ȩԱ������
                newcap.Text = (boref2 * addlengthfset * math * domainUpDown1i).ToString("0.00");

            if (radioButton1.Checked) //��ܭp��ƶq
            {
                cap.Text = (boref2 * lengthf * math * domainUpDown1i).ToString("0.00"); //�p��e�n
            }
            else if (radioButton2.Checked) //��ܭp����|
            {
                bore.Text = (Math.Sqrt(capf / (lengthf * math * domainUpDown1i))).ToString("0.00"); //�p����|
            }
            else if (radioButton3.Checked)  //��ܭp���{
            {
                length.Text = (capf / (boref2 * math * domainUpDown1i)).ToString("0.00"); //�p���{
            }
        }

        private void clearbutton_Click(object sender, EventArgs e)
        {
            //�M���Ҧ����A
            bore.Text = "";
            length.Text = "";
            cap.Text = "";
            newbore.Text = "";
            addlength.Text = "";
            newcap.Text = "";
            multiplier.Text = "";
            multiplierCC.Text = "";
            percent.Text = "";
            domainUpDown1.SelectedIndex = 8;

            
            foreach (var radioButton in groupBox1.Controls.OfType<RadioButton>()) //�ϥ�foreach���j��groupbox1��radioButton�����Ŀ�
            {
                radioButton.Checked = false;
            }

            if (radioButton1.Checked) //��ܭp��ƶq
            {
                bore.ReadOnly = false;
                length.ReadOnly = false;
                cap.ReadOnly = true;
            }
            else if (radioButton2.Checked) //��ܭp����|
            {
                bore.ReadOnly = true;
                length.ReadOnly = false;
                cap.ReadOnly = false;
            }
            else if (radioButton3.Checked)  //��ܭp���{
            {
                bore.ReadOnly = false;
                length.ReadOnly = true;
                cap.ReadOnly = false;
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Use no more than one assignment when you test this code.
            //string target = "C:\\Program Files\\Microsoft Visual Studio\\INSTALL.HTM";
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", "https://github.com/timliucode");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}