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

namespace VRMS___Security__12_01_21_
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //DATABASE
        OdbcConnection con = new OdbcConnection("dsn=capstone");

        //TIME AND DATE
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            timer.Start();
            lblDate.Text = DateTime.Now.ToString("MM - dd - yyyy");
        }

        //TIMER
        private void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            timer.Start();
        }

        //CANCEL
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        //FORM LOAD
        private void Login_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
            txtPass.UseSystemPasswordChar = true;
        }

        //SHOW PASSWORD - CHECKBOX
        private void showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (showpass.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        //SHOW PASSWORD - LABEL
        private void lblShow_Click(object sender, EventArgs e)
        {
            if (showpass.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        //LOGIN
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser.Text.Length == 0 || txtPass.Text.Length == 0)
                {
                    MessageBox.Show("Username/Password cannot be blank", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                    txtUser.Clear();
                    txtPass.Clear();
                    txtUser.Focus();
                }

                else if (txtUser.Text.Length <= 4 || txtPass.Text.Length <= 4)
                {
                    MessageBox.Show("Username/Password cannot be less 4 Characters", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                    txtPass.Clear();
                    txtUser.Clear();
                    txtUser.Focus();
                }

                else
                {
                    VRMS___Security__12_01_21_.EntryScan scan = new EntryScan();
                    VRMS___Security__12_01_21_.Exit scan2 = new Exit();
                    VRMS___Security__12_01_21_.LoadingScreen ls = new LoadingScreen();

                    OdbcCommand cmd = new OdbcCommand("SELECT fullname, level, admin_id, status FROM accounts WHERE username='" + txtUser.Text + "' AND password='" + txtPass.Text + "';", con);
                    OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adptr.Fill(dt);
                    lblName.Text = dt.Rows[0][0].ToString();
                    lblAccess.Text = dt.Rows[0][1].ToString();

                    scan.lblCurrent2.Text = dt.Rows[0][0].ToString();
                    scan2.lblCurrent3.Text = dt.Rows[0][0].ToString();
                    //scan2.lblCurrent3.Text = scan.lblCurrent2.Text;
                    // dt.Rows[0][0].ToString();

                    lblAdminID.Text = dt.Rows[0][2].ToString();

                    scan.lblAdminID2.Text = dt.Rows[0][2].ToString();
                    scan2.lblAdminID3.Text = dt.Rows[0][2].ToString();
                    //scan2.lblAdminID3.Text = scan.lblAdminID2.Text;

                    lblStatus.Text = dt.Rows[0][3].ToString();

                    con.Close();

                    if (lblStatus.Text == "ONLINE")
                    {
                        MessageBox.Show("This account is already login", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUser.Text = "";
                        txtPass.Text = "";
                    }
                    else
                    {
                        if (lblAccess.Text == "SECURITY")
                        {
                            try
                            {
                                ls.TopLevel = true;
                                scan.Show();
                                ls.ShowDialog();

                                this.Hide();
                                con.Open();
                                OdbcCommand cmd1 = new OdbcCommand();
                                cmd1 = con.CreateCommand();
                                cmd1.CommandText = "INSERT INTO loghistory(admin_id, fullname, access, date, time, event) VALUES (?, ?, ?, ?, ?, ?)";
                                cmd1.Parameters.Add("@admin_id", OdbcType.VarChar).Value = dt.Rows[0][2].ToString();
                                cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = dt.Rows[0][0].ToString();
                                cmd1.Parameters.Add("@access", OdbcType.VarChar).Value = "SECURITY";
                                cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                                cmd1.Parameters.Add("@time", OdbcType.VarChar).Value = lblTime.Text;
                                cmd1.Parameters.Add("@event", OdbcType.VarChar).Value = "LOGIN";
                                cmd1.ExecuteNonQuery();
                                con.Close();
                                login();
                            }

                            catch (Exception ex)

                            {
                                
                            }
                        }

                        else
                        {
                            con.Close();
                            MessageBox.Show("THIS ACCOUNT IS REGISTERED AS ADMIN", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPass.Clear();
                            txtUser.Clear();
                            txtUser.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("INVALID USERNAME AND PASSWORD","WARNING!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtPass.Clear();
                txtUser.Clear();
                txtUser.Focus();
            }
        }

        //LOGIN FUNCTION
        public void login()
        {
            try
            {
                con.Open();
                OdbcCommand cmd1 = new OdbcCommand();
                cmd1 = con.CreateCommand();
                cmd1.CommandText = "UPDATE accounts SET status = 'ONLINE' WHERE admin_id='" + lblAdminID.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();
                this.Hide();
                lblName.Text = "";
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //FUNCTION KEYS
        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            //LOGIN
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    VRMS___Security__12_01_21_.EntryScan scan = new EntryScan();
                    VRMS___Security__12_01_21_.LoadingScreen ls = new LoadingScreen();
                    OdbcCommand cmd = new OdbcCommand("SELECT fullname, level, admin_id, status FROM accounts WHERE username='" + txtUser.Text + "' AND password='" + txtPass.Text + "';", con);
                    OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adptr.Fill(dt);
                    lblName.Text = dt.Rows[0][0].ToString();
                    lblAccess.Text = dt.Rows[0][1].ToString();
                    scan.lblCurrent2.Text = dt.Rows[0][0].ToString(); ;

                    scan.lblCurrent2.Text = dt.Rows[0][0].ToString();

                    lblAdminID.Text = dt.Rows[0][2].ToString();

                    scan.lblAdminID2.Text = dt.Rows[0][2].ToString();

                    lblStatus.Text = dt.Rows[0][3].ToString();

                    con.Close();

                    if (lblStatus.Text == "ONLINE")
                    {
                        MessageBox.Show("This account is already login", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUser.Text = "";
                        txtPass.Text = "";
                    }
                    else
                    {
                        if (lblAccess.Text == "SECURITY")
                        {
                            try
                            {
                                ls.TopLevel = true;
                                scan.Show();
                                ls.ShowDialog();

                                this.Hide();
                                con.Open();
                                OdbcCommand cmd1 = new OdbcCommand();
                                cmd1 = con.CreateCommand();
                                cmd1.CommandText = "INSERT INTO loghistory(admin_id, fullname, access, date, time, event) VALUES (?, ?, ?, ?, ?, ?)";
                                cmd1.Parameters.Add("@admin_id", OdbcType.VarChar).Value = dt.Rows[0][2].ToString();
                                cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = dt.Rows[0][0].ToString();
                                cmd1.Parameters.Add("@access", OdbcType.VarChar).Value = "SECURITY";
                                cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                                cmd1.Parameters.Add("@time", OdbcType.VarChar).Value = lblTime.Text;
                                cmd1.Parameters.Add("@event", OdbcType.VarChar).Value = "LOGIN";
                                cmd1.ExecuteNonQuery();
                                con.Close();
                                login();
                            }

                            catch (Exception ex)
                            {
                                con.Close();
                            }
                        }

                        else
                        {
                            con.Close();
                            MessageBox.Show("THIS ACCOUNT IS REGISTERED AS ADMIN", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPass.Clear();
                            txtUser.Clear();
                            txtUser.Focus();
                        }
                    }
                }

                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show("INVALID USERNAME AND PASSWORD", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Clear();
                    txtUser.Clear();
                    txtUser.Focus();
                }
            }

            //CLOSE
            if (e.KeyCode == Keys.Escape)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }

        //SPECIAL CHARACTER VALIDATION
        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Invalid Characters", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtPass.Clear();
                txtUser.Clear();
                txtUser.Focus();
            }
        }

        //FUNCTION KEYS
        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            //CLOSE
            if (e.KeyCode == Keys.Escape)
            {
                System.Windows.Forms.Application.ExitThread();
            }

            //LOGIN
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    VRMS___Security__12_01_21_.EntryScan scan = new EntryScan();
                    OdbcCommand cmd = new OdbcCommand("SELECT fullname, level, admin_id, status FROM accounts WHERE username='" + txtUser.Text + "' AND password='" + txtPass.Text + "';", con);
                    OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adptr.Fill(dt);
                    lblName.Text = dt.Rows[0][0].ToString();
                    lblAccess.Text = dt.Rows[0][1].ToString();
                    scan.lblCurrent2.Text = dt.Rows[0][0].ToString(); ;

                    scan.lblCurrent2.Text = dt.Rows[0][0].ToString();

                    lblAdminID.Text = dt.Rows[0][2].ToString();

                    scan.lblAdminID2.Text = dt.Rows[0][2].ToString();

                    lblStatus.Text = dt.Rows[0][3].ToString();

                    con.Close();

                    if (lblStatus.Text == "ONLINE")
                    {
                        MessageBox.Show("This account is already login", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUser.Text = "";
                        txtPass.Text = "";
                    }
                    else
                    {
                        if (lblAccess.Text == "SECURITY")
                        {
                            try
                            {

                                scan.Show();
                                this.Hide();
                                con.Open();
                                OdbcCommand cmd1 = new OdbcCommand();
                                cmd1 = con.CreateCommand();
                                cmd1.CommandText = "INSERT INTO loghistory(admin_id, fullname, access, date, time, event) VALUES (?, ?, ?, ?, ?, ?)";
                                cmd1.Parameters.Add("@admin_id", OdbcType.VarChar).Value = dt.Rows[0][2].ToString();
                                cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = dt.Rows[0][0].ToString();
                                cmd1.Parameters.Add("@access", OdbcType.VarChar).Value = "SECURITY";
                                cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                                cmd1.Parameters.Add("@time", OdbcType.VarChar).Value = lblTime.Text;
                                cmd1.Parameters.Add("@event", OdbcType.VarChar).Value = "LOGIN";
                                cmd1.ExecuteNonQuery();
                                con.Close();
                                login();
                            }

                            catch (Exception ex)
                            {
                                con.Close();
                            }
                        }

                        else
                        {
                            con.Close();
                            MessageBox.Show("THIS ACCOUNT IS REGISTERED AS ADMIN", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPass.Clear();
                            txtUser.Clear();
                            txtUser.Focus();
                        }
                    }
                }

                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show("INVALID USERNAME AND PASSWORD", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Clear();
                    txtUser.Clear();
                    txtUser.Focus();
                }
            }
        }

    }
}
