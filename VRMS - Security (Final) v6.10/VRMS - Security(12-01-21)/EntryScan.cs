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
using System.Security.Cryptography;
using System.IO;  

namespace VRMS___Security__12_01_21_
{
    public partial class EntryScan : Form
    {
        public EntryScan()
        {
            InitializeComponent();
        }

        //DATABASE
        OdbcConnection con = new OdbcConnection("dsn=capstone");
        public static int counter = 5;

        //SCANNING
        private void txtScan_TextChanged(object sender, EventArgs e)
        {
            if (txtScan.TextLength >= 24)
            {
                fetch();
            }

            if (txtScan.TextLength >= 1)
            {
                notif.ForeColor = Color.Red;
                notif.Text = "Please wait for a few seconds.";
            }

            else if(txtScan.Text == "")
            {
                notif.ForeColor = System.Drawing.Color.Green;
                notif.Text = "QR Scanner Ready to Use.";
            }
        }

        //FORM LOAD
        private void EntryScan_Load(object sender, EventArgs e)
        {
            display();
            txtScan.Focus();
            notif.ForeColor = System.Drawing.Color.Green;
            notif.Text = "QR Scanner Ready to Use.";
        }

        //DISPLAY ENTRY MONITORING DATA-----
        public void display()
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand("SELECT qrtext as 'VEHICLE ID', owner_id as 'OPERATOR ID', plate_num as 'PLATE NUMBER', type as 'TYPE', date as 'DATE', time_in as 'TIME', event as 'EVENT' FROM entry_monitoring ORDER BY time_in DESC;", con);
                OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                DataSet ds = new DataSet();
                adptr.Fill(ds, "Empty");
                dgvLeft.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        //DATABASE FETCH
        private void fetch()
        {
            string text = txtScan.Text;
            string dec = DecryptString("AAECAwQFBgcICQoLDA0ODw==", text);
            try
            {
                OdbcCommand cmdd = new OdbcCommand("SELECT COUNT(*) FROM registered_vehicles WHERE qrtext='" + dec + "'", con);
                OdbcDataAdapter adptrr = new OdbcDataAdapter(cmdd);
                DataTable dtt = new DataTable();
                adptrr.Fill(dtt);
                string i;

                i = dtt.Rows[0][0].ToString();
                int j = int.Parse(i);

                if (txtScan.Text.Length >= 24)
                {
                    if (j == 0)
                    {
                        VRMS___Security__12_01_21_.MessageForExit mess = new MessageForExit();
                        mess.ShowDialog();
                        txtScan.Clear();
                        txtScan.Focus();

                        notif.ForeColor = System.Drawing.Color.Green;
                        notif.Text = "QR Scanner Ready to Use.";
                    }

                    else
                    {
                        try
                        {
                            OdbcCommand cmd = new OdbcCommand("SELECT registered_owners.owner_id,registered_owners.fname,registered_owners.mname,registered_owners.lname,registered_owners.suf,registered_vehicles.qrtext,registered_vehicles.type,registered_vehicles.plate_num FROM registered_owners INNER JOIN registered_vehicles ON registered_owners.owner_id=registered_vehicles.owner_id WHERE registered_vehicles.qrtext='" + dec + "'", con);
                            OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            adptr.Fill(dt);

                            lblOwnerID.Text = dt.Rows[0][0].ToString();

                            fname.Text = dt.Rows[0][1].ToString();
                            mname.Text = dt.Rows[0][2].ToString();
                            lname.Text = dt.Rows[0][3].ToString();
                            suf.Text = dt.Rows[0][4].ToString();
                            lblQrtext.Text = dt.Rows[0][5].ToString();
                            lblType.Text = dt.Rows[0][6].ToString();
                            lblPlateNumber.Text = dt.Rows[0][7].ToString();
                            lblPlatenum.Text = dt.Rows[0][7].ToString();

                            lblFullname.Text = fname.Text + " " + mname.Text + " " + lname.Text + " " + suf.Text;
                            lblOperatorName.Text = fname.Text + " " + lname.Text + " " + suf.Text;

                            con.Close();
                            Olic();
                            display();

                            timer = new System.Windows.Forms.Timer();
                            timer.Tick += new EventHandler(timer_Tick);
                            timer.Interval = 1000; // 1 second
                            timer.Start();
                            count.Text = counter.ToString();

                            if (txtScan.TextLength == 24)
                            {
                                //insert();
                            }
                        }

                        catch (Exception ex)
                        {
                            con.Close();
                            if (txtScan.Text == "")
                            {
                                notif.ForeColor = System.Drawing.Color.Green;
                                notif.Text = "QR Scanner Ready to Use.";
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                con.Close();
                if (txtScan.Text == "")
                {
                    notif.ForeColor = System.Drawing.Color.Green;
                    notif.Text = "QR Scanner Ready to Use.";
                }
            }
        }

        //INSERT TO ENTRY DATABASE
        public void insert()
        {
            try
            {
                con.Open();
                OdbcCommand cmd = new OdbcCommand();
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO entry_monitoring(qrtext,owner_id,plate_num,type,date,time_in,event)VALUES(?,?,?,?,?,?,?);";
                cmd.Parameters.Add("@qrtext", OdbcType.VarChar).Value = lblQrtext.Text;
                cmd.Parameters.Add("@owner_id", OdbcType.VarChar).Value = lblOwnerID.Text;
                cmd.Parameters.Add("@plate_num", OdbcType.VarChar).Value = lblPlatenum.Text;
                cmd.Parameters.Add("@type", OdbcType.VarChar).Value = lblType.Text;
                cmd.Parameters.Add("@date", OdbcType.VarChar).Value = DateTime.Now.ToString("MM-dd-yyyy");
                cmd.Parameters.Add("@time_in", OdbcType.VarChar).Value = DateTime.Now.ToString("hh:mm:ss");
                cmd.Parameters.Add("@event", OdbcType.VarChar).Value = "ENTRY";

                if (cmd.ExecuteNonQuery() == 1)
                {
                    //insert();
                }

                con.Close();
            }

            catch (Exception ex)
            {
                con.Close();
                showMessage call = new showMessage();
                call.ShowDialog();
            }
        }

        //INSERT TO VEHICLE LOGS
        public void vLogs()
        {
            try
            {
                con.Open();
                OdbcCommand cmd = new OdbcCommand();
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO vehicle_log(qrtext, owner_id, plate_num, type, time_in, time_out, date)VALUES(?,?,?,?,?,?,?);";
                cmd.Parameters.Add("@qrtext", OdbcType.VarChar).Value = lblQrtext.Text;
                cmd.Parameters.Add("@owner_id", OdbcType.VarChar).Value = lblOwnerID.Text;
                cmd.Parameters.Add("@plate_num", OdbcType.VarChar).Value = lblPlatenum.Text;
                cmd.Parameters.Add("@type", OdbcType.VarChar).Value = lblType.Text;
                cmd.Parameters.Add("@time_in", OdbcType.VarChar).Value = DateTime.Now.ToString("hh:mm:ss");
                cmd.Parameters.Add("@time_out", OdbcType.VarChar).Value = "CURRENTLY INSIDE";
                cmd.Parameters.Add("@date", OdbcType.VarChar).Value = DateTime.Now.ToString("MM-dd-yyyy");

                if (cmd.ExecuteNonQuery() == 1)
                {
                    //insert();
                }

                con.Close();
            }

            catch (Exception ex)
            {
                con.Close();
                if (txtScan.TextLength >= 24)
                {
                    MessageForExit mess = new MessageForExit();
                    mess.ShowDialog();
                }
            }
        }

        //LOGOUT-----
        private void btnLogout_Click(object sender, EventArgs e)
        {
         DialogResult ask = MessageBox.Show("Do you want to logout " + lblCurrent2.Text + "? ", "WARNING!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
         if (ask == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    OdbcCommand cmd1 = new OdbcCommand();
                    cmd1 = con.CreateCommand();
                    cmd1.CommandText = "INSERT INTO loghistory (admin_id, fullname, access, date, time, event) VALUES (?, ?, ?, ?, ?, ?)";
                    cmd1.Parameters.Add("@admin_id", OdbcType.VarChar).Value = lblAdminID2.Text;
                    cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = lblCurrent2.Text;
                    cmd1.Parameters.Add("@access", OdbcType.VarChar).Value = "OSAS";
                    cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                    cmd1.Parameters.Add("@time", OdbcType.VarChar).Value = lblTime.Text;
                    cmd1.Parameters.Add("@event", OdbcType.VarChar).Value = "LOGOUT";
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    logout();
                    this.Hide();
                    lblCurrent2.Text = "";
                }
                catch (Exception ex)
                {
                    con.Close();
                    //MessageBox.Show(ex.Message);
                }
            }
         else
         {
             txtScan.Clear();
             txtScan.Focus();
         }
        }

        //LOGOUT FUNCTION-----
        public void logout()
        {
            try
            {
                con.Open();
                OdbcCommand cmd1 = new OdbcCommand();
                cmd1 = con.CreateCommand();
                cmd1.CommandText = "UPDATE accounts SET status = 'OFFLINE' WHERE admin_id = '" + lblAdminID2.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();

                this.Hide();
                Login call = new Login();
                call.Show();
                lblCurrent2.Text = "";
            }
            catch (Exception ex)
            {
                con.Close();
                //MessageBox.Show(ex.Message);
            }
        }

        //OWNER PIC
        public void Olic()
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand("SELECT img FROM owner_pic WHERE owner_id='" + lblOwnerID.Text + "';", con);
                OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                DataTable dt = new DataTable();
                adptr.Fill(dt);
                byte[] image = ((byte[])dt.Rows[0][0]);
                TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
                Bitmap bitmap = (Bitmap)tc.ConvertFrom(image);
                pbImage.Image = bitmap;
                con.Close();
            }
            catch (Exception ex)
            {
                pbImage.Image = null;
                con.Close();
            }
        }

        //TIMER
        private void ItsTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToString("MM-dd-yyyy");
        }

        //COUNTDOWN TIMER-----
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                string end = "0";
                counter--;
                if (counter == 0)
                {
                    txtScan.Focus();
                    timer.Stop();
                }

                count.Text = counter.ToString();

                if (count.Text == end)
                {
                    insert();
                    vLogs();
                    overall();
                    overall_inside();
                    timer.Stop();

                    lblQrtext.Text = "";
                    lblOwnerID.Text = "";
                    lblPlatenum.Text = "";
                    lblType.Text = "";
                    lblFullname.Text = "";
                    lblPlateNumber.Text = "XXX 000";
                    lblOperatorName.Text = "Proprietary Name";
                    pbImage.Image = null;

                    counter = 5;
                    count.Text = "5";

                    txtScan.Clear();
                    txtScan.Focus();

                    notif.ForeColor = Color.Green;
                    notif.Text = "QR Scanner Ready to Use.";

                    display();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        //INSERT TO OVERALL MONITORING-----
        private void overall()
        {
            try
            {
                con.Open();
                OdbcCommand cmd1 = new OdbcCommand();
                cmd1 = con.CreateCommand();
                cmd1.CommandText = "INSERT INTO overall_monitoring (class, type, plate_num, date) VALUES (?, ?, ?, ?)";
                cmd1.Parameters.Add("@class", OdbcType.VarChar).Value = "Proprietary";
                cmd1.Parameters.Add("@type", OdbcType.VarChar).Value = lblType.Text;
                cmd1.Parameters.Add("@plate_num", OdbcType.VarChar).Value = lblPlateNumber.Text;
                cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                //MessageBox.Show(ex.Message);
            }
        }

        //INSERT INTO OVERALL INSIDE-----
        public void overall_inside()
        {
            try
            {
                con.Open();
                OdbcCommand cmd1 = new OdbcCommand();
                cmd1 = con.CreateCommand();
                cmd1.CommandText = "INSERT INTO overall_inside(class, type, plate_num, date, entry_id) VALUES (?, ?, ?, ?, ?)";
                cmd1.Parameters.Add("@class", OdbcType.VarChar).Value = "Proprietary";
                cmd1.Parameters.Add("@type", OdbcType.VarChar).Value = lblType.Text;
                cmd1.Parameters.Add("@plate_num", OdbcType.VarChar).Value = lblPlateNumber.Text;
                cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                cmd1.Parameters.Add("@entry_id", OdbcType.VarChar).Value = lblOwnerID.Text;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                //MessageBox.Show(ex.Message);
            }
        }

        //SWITCH TO EXIT using KEYS-----
        private void txtScan_KeyDown(object sender, KeyEventArgs e)
        {
            //LOGOUT
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult ask = MessageBox.Show("Do you want to logout " + lblCurrent2.Text + "? ", "WARNING!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (ask == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        OdbcCommand cmd1 = new OdbcCommand();
                        cmd1 = con.CreateCommand();
                        cmd1.CommandText = "INSERT INTO loghistory (admin_id, fullname, access, date, time, event) VALUES (?, ?, ?, ?, ?, ?)";
                        cmd1.Parameters.Add("@admin_id", OdbcType.VarChar).Value = lblAdminID2.Text;
                        cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = lblCurrent2.Text;
                        cmd1.Parameters.Add("@access", OdbcType.VarChar).Value = "SECURITY";
                        cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                        cmd1.Parameters.Add("@time", OdbcType.VarChar).Value = lblTime.Text;
                        cmd1.Parameters.Add("@event", OdbcType.VarChar).Value = "LOGOUT";
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        logout();
                        this.Hide();
                        lblCurrent2.Text = "";
                    }

                    catch (Exception ex)
                    {
                        con.Close();
                        //MessageBox.Show(ex.Message);
                    }
                }

                else
                {
                    txtScan.Clear();
                    txtScan.Focus();
                }
            }

            //SWITCH TO EXIT MONITORING
            if (e.KeyCode == Keys.F2)
            {
                VRMS___Security__12_01_21_.Exit exit = new Exit();
                exit.lblCurrent3.Text = lblCurrent2.Text;
                exit.lblAdminID3.Text = lblAdminID2.Text;

                exit.Show();
                this.Hide();
            }

            //SWITCH TO VISITOR ENTRY
            if (e.KeyCode == Keys.F3)
            {
                VRMS___Security__12_01_21_.Visitor vis = new Visitor();
                vis.lblCurrent4.Text = lblCurrent2.Text;
                vis.lblAdminID4.Text = lblAdminID2.Text;

                vis.Show();
                this.Hide();
            }

            //RELOAD
            if (e.KeyCode == Keys.F5)
            {
                txtScan.Clear();
                txtScan.Focus();
                display();
            }
        }

        //DECRYPT FUNCTION
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
