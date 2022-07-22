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
    public partial class Exit : Form
    {
        public Exit()
        {
            InitializeComponent();
        }

        //DATABASE
        OdbcConnection con = new OdbcConnection("dsn=capstone");
        public static int counter = 5;

        //FORM LOAD
        private void Exit_Load(object sender, EventArgs e)
        {
            display();
            txtScan2.Focus();
            notif.ForeColor = System.Drawing.Color.Green;
        }

        // DISPLAY EXIT MONITORING TABLE
        public void display()
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand("SELECT qrtext as 'VEHICLE ID', owner_id as 'OPERATOR ID', plate_num as 'PLATE NUMBER', type as 'TYPE', date as 'DATE', time_out as 'TIME', event as 'EVENT' FROM exit_monitoring ORDER BY time_out DESC;", con);
                OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                DataSet ds = new DataSet();
                adptr.Fill(ds, "Empty");
                dgvExit.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        // TEXT CHANGED
        private void txtScan2_TextChanged(object sender, EventArgs e)
        {
            fetch();

            if (txtScan2.TextLength >= 1)
            {
                notif.ForeColor = Color.Red;
                notif.Text = "Please wait for a few seconds.";
            }

            else if (txtScan2.Text == "")
            {
                notif.ForeColor = Color.Green;
                notif.Text = "QR Scanner Ready to Use.";
            }
        }

        // FETCHING OF DATA
        private void fetch()
        {
            try
            {
                string text = txtScan2.Text;
                string dec = DecryptString("AAECAwQFBgcICQoLDA0ODw==", text);
                OdbcCommand cmdd = new OdbcCommand("SELECT COUNT(*) FROM entry_monitoring WHERE qrtext  like '" + dec + "';", con);
                OdbcDataAdapter adptrr = new OdbcDataAdapter(cmdd);
                DataTable dtt = new DataTable();
                adptrr.Fill(dtt);
                string i;

                i = dtt.Rows[0][0].ToString();
                int j = int.Parse(i);

                if (txtScan2.Text.Length >= 7)
                {
                    if (j == 0)
                    {
                        VRMS___Security__12_01_21_.MessageForExit mess = new MessageForExit();
                        mess.ShowDialog();
                        txtScan2.Clear();
                        txtScan2.Focus();
                    }

                    else
                    {
                        OdbcCommand cmd = new OdbcCommand("SELECT entry_monitoring.qrtext, entry_monitoring.owner_id, entry_monitoring.plate_num, entry_monitoring.type, entry_monitoring.date, entry_monitoring.time_in, registered_owners.fname, registered_owners.mname, registered_owners.lname, registered_owners.suf FROM entry_monitoring INNER JOIN registered_owners ON entry_monitoring.owner_id=registered_owners.owner_id WHERE entry_monitoring.qrtext = '" + dec + "';", con);

                        OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adptr.Fill(dt);

                        lblQrtext.Text = dt.Rows[0][0].ToString();
                        lblOwnerID.Text = dt.Rows[0][1].ToString();
                        lblPlateNumber.Text = dt.Rows[0][2].ToString();
                        lblType.Text = dt.Rows[0][3].ToString();
                        Datelbl.Text = dt.Rows[0][4].ToString();
                        Timelbl.Text = dt.Rows[0][5].ToString();
                        fname.Text = dt.Rows[0][6].ToString();
                        mname.Text = dt.Rows[0][7].ToString();
                        lname.Text = dt.Rows[0][8].ToString();
                        suf.Text = dt.Rows[0][9].ToString();


                        lblOperatorName.Text = fname.Text + " " + lname.Text + " " + suf.Text;
                        //lblOperatorName.Text = fname.Text + " " + mname.Text + " " + lname.Text + " " + suf.Text;

                        con.Close();
                        picture();

                        timer = new System.Windows.Forms.Timer();
                        timer.Tick += new EventHandler(timer_Tick);
                        timer.Interval = 1000; // 1 second
                        timer.Start();
                        count.Text = counter.ToString();

                        if (txtScan2.TextLength == 7)
                        {
                            //insert(); 
                        }
                        con.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                con.Close();
                if (txtScan2.Text == "")
                {
                    notif.ForeColor = Color.Green;
                    notif.Text = "QR Scanner Ready to Use.";
                }
            }
        }

        // PICTURE
        public void picture()
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

        //TIMER CLICK
        private void timer2_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToString("MM-dd-yyyy");
        }

        //COUNTDOWN TIMER
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                string end = "0";
                counter--;
                if (counter == 0)
                {
                    txtScan2.Focus();
                    timer.Stop();
                }

                count.Text = counter.ToString();

                if (count.Text == end)
                {
                    insert();
                    vLogs();
                    delete();
                    delete_overall();
                    timer.Stop();

                    lblQrtext.Text = "";
                    lblOwnerID.Text = "";
                    lblType.Text = "";
                    lblPlateNumber.Text = "XXX 000";
                    lblOperatorName.Text = "Proprietary Name";
                    pbImage.Image = null;

                    counter = 5;
                    count.Text = "5";
                   

                        con.Close();
                        txtScan2.Clear();
                        txtScan2.Focus();

                        notif.ForeColor = Color.Green;
                        notif.Text = "QR Scanner Ready to Use.";

                        display();
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //INSERT TO EXIT DATABASE
        public void insert()
        {
            try
            {
                con.Open();
                OdbcCommand cmd = new OdbcCommand();
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO exit_monitoring(qrtext,owner_id,plate_num,type,date,time_out,event)VALUES(?,?,?,?,?,?,?);";
                cmd.Parameters.Add("@qrtext", OdbcType.VarChar).Value = lblQrtext.Text;
                cmd.Parameters.Add("@owner_id", OdbcType.VarChar).Value = lblOwnerID.Text;
                cmd.Parameters.Add("@plate_num", OdbcType.VarChar).Value = lblPlateNumber.Text;
                cmd.Parameters.Add("@type", OdbcType.VarChar).Value = lblType.Text;
                cmd.Parameters.Add("@date", OdbcType.VarChar).Value = DateTime.Now.ToString("MM-dd-yyyy");
                cmd.Parameters.Add("@time_out", OdbcType.VarChar).Value = DateTime.Now.ToString("hh:mm:ss");
                cmd.Parameters.Add("@event", OdbcType.VarChar).Value = "EXIT";

                if (cmd.ExecuteNonQuery() == 1)
                {
                    //insert();
                }
               
                con.Close();
            }

            catch (Exception ex)
            {
                MessageForExit mess = new MessageForExit();
                mess.ShowDialog();
                con.Close();
               
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
                cmd.CommandText = "UPDATE vehicle_log SET time_out = '" + lblTime.Text + "' WHERE owner_id =  '" + lblOwnerID.Text + "';";

                if (cmd.ExecuteNonQuery() == 1)
                {
                    //insert();
                }

                con.Close();
            }

            catch (Exception ex)
            {
                con.Close();
            }
        }

        //DELETE DATA FROM ENTRY MONIROTING
        public void delete()
        {
            string text = txtScan2.Text;
            string dec = DecryptString("AAECAwQFBgcICQoLDA0ODw==", text);
            try
            {
                con.Open();
                OdbcCommand cmd = new OdbcCommand();
                cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM entry_monitoring WHERE qrtext = '" + dec + "'";
                cmd.ExecuteNonQuery();
                con.Close();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    //insert();
                }

                con.Close();
            }

            catch (Exception ex)
            {
                con.Close();
            }
        }

        //DELETE DATA FROM OVERALL INSIDE
        public void delete_overall()
        {
            try
            {
                con.Open();
                OdbcCommand cmd = new OdbcCommand();
                cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM overall_inside WHERE entry_id = '" + lblOwnerID.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    //insert();
                }

                con.Close();
            }

            catch (Exception ex)
            {
                con.Close();
            }
        }

        // LOGOUT
        private void btnLogout_Click(object sender, EventArgs e)
        {
         DialogResult ask = MessageBox.Show("Do you want to logout " + lblCurrent3.Text + "? ", "WARNING!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ask == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    OdbcCommand cmd1 = new OdbcCommand();
                    cmd1 = con.CreateCommand();
                    cmd1.CommandText = "INSERT INTO loghistory (admin_id, fullname, access, date, time, event) VALUES (?, ?, ?, ?, ?, ?)";
                    cmd1.Parameters.Add("@admin_id", OdbcType.VarChar).Value = lblAdminID3.Text;
                    cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = lblCurrent3.Text;
                    cmd1.Parameters.Add("@access", OdbcType.VarChar).Value = "OSAS";
                    cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                    cmd1.Parameters.Add("@time", OdbcType.VarChar).Value = lblTime.Text;
                    cmd1.Parameters.Add("@event", OdbcType.VarChar).Value = "LOGOUT";
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    logout();
                    this.Hide();
                    lblCurrent3.Text = "";
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                txtScan2.Clear();
                txtScan2.Focus();
            }
        }

        //LOGOUT FUNCTION
        public void logout()
        {
            try
            {
                con.Open();
                OdbcCommand cmd1 = new OdbcCommand();
                cmd1 = con.CreateCommand();
                cmd1.CommandText = "UPDATE accounts SET status = 'OFFLINE' WHERE admin_id = '" + lblAdminID3.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();

                this.Hide();
                Login call = new Login();
                call.Show();
                lblCurrent3.Text = "";
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //SWITCH TO ENTRY using KEYS
        private void txtScan2_KeyDown(object sender, KeyEventArgs e)
        {
            //SWITCH TO ENTRY MONITORING
            if (e.KeyCode == Keys.F2)
            {
                VRMS___Security__12_01_21_.EntryScan exit = new EntryScan();
                exit.lblCurrent2.Text = lblCurrent3.Text;
                exit.lblAdminID2.Text = lblAdminID3.Text;

                exit.Show();
                this.Hide();
            }

            //LOGOUT
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult ask = MessageBox.Show("Do you want to logout " + lblCurrent3.Text + "? ", "WARNING!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (ask == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        OdbcCommand cmd1 = new OdbcCommand();
                        cmd1 = con.CreateCommand();
                        cmd1.CommandText = "INSERT INTO loghistory (admin_id, fullname, access, date, time, event) VALUES (?, ?, ?, ?, ?, ?)";
                        cmd1.Parameters.Add("@admin_id", OdbcType.VarChar).Value = lblAdminID3.Text;
                        cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = lblCurrent3.Text;
                        cmd1.Parameters.Add("@access", OdbcType.VarChar).Value = "SECURITY";
                        cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                        cmd1.Parameters.Add("@time", OdbcType.VarChar).Value = lblTime.Text;
                        cmd1.Parameters.Add("@event", OdbcType.VarChar).Value = "LOGOUT";
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        logout();
                        this.Hide();
                        lblCurrent3.Text = "";
                    }
                    
                    catch (Exception ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message);
                    }
                }

                else
                {
                    txtScan2.Clear();
                    txtScan2.Focus();
                }
            }

            //RELOAD
            if (e.KeyCode == Keys.F5)
            {
                txtScan2.Clear();
                txtScan2.Focus();
                display();
            }

            //SWITCH TO VISITOR ENTRY
            if (e.KeyCode == Keys.F3)
            {
                VRMS___Security__12_01_21_.Visitor vis = new Visitor();
                vis.lblCurrent4.Text = lblCurrent3.Text;
                vis.lblAdminID4.Text = lblAdminID3.Text;

                vis.Show();
                this.Hide();
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
