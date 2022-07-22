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
    public partial class Visitor : Form
    {
        public Visitor()
        {
            InitializeComponent();
        }

        //DATABASE
        OdbcConnection con = new OdbcConnection("dsn=capstone");
        string XPlatenum = "9999";
        string holder_X, holder_id;
        string holds;

        //FUNCTION KEYS
        private void txtScan3_KeyDown(object sender, KeyEventArgs e)
        {
            //LOGOUT CURRENT USER
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult ask = MessageBox.Show("Do you want to logout " + lblCurrent4.Text + "? ", "WARNING!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (ask == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        OdbcCommand cmd1 = new OdbcCommand();
                        cmd1 = con.CreateCommand();
                        cmd1.CommandText = "INSERT INTO loghistory (admin_id, fullname, access, date, time, event) VALUES (?, ?, ?, ?, ?, ?)";
                        cmd1.Parameters.Add("@admin_id", OdbcType.VarChar).Value = lblAdminID4.Text;
                        cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = lblCurrent4.Text;
                        cmd1.Parameters.Add("@access", OdbcType.VarChar).Value = "SECURITY";
                        cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                        cmd1.Parameters.Add("@time", OdbcType.VarChar).Value = lblTime.Text;
                        cmd1.Parameters.Add("@event", OdbcType.VarChar).Value = "LOGOUT";
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        logout();
                        this.Hide();
                        lblCurrent4.Text = "";
                    }

                    catch (Exception ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message);
                    }
                }

                else
                {
                    txtScan3.Focus();
                    txtScan3.Clear();
                }
            }

            //SWITCH TO REGULAR ENTRY
            if (e.KeyCode == Keys.F3)
            {
                VRMS___Security__12_01_21_.EntryScan entry = new EntryScan();
                entry.lblCurrent2.Text = lblCurrent4.Text;
                entry.lblAdminID2.Text = lblAdminID4.Text;

                entry.Show();
                this.Hide();
            }

            //REFRESH DATAGRIDVIEW
            if (e.KeyCode == Keys.F5)
            {
                txtScan3.Clear();
                txtScan3.Focus();
                display();
            }
        }

        //LOGOUT USING BUTTON
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult ask = MessageBox.Show("Do you want to logout " + lblCurrent4.Text + "? ", "WARNING!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ask == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    OdbcCommand cmd1 = new OdbcCommand();
                    cmd1 = con.CreateCommand();
                    cmd1.CommandText = "INSERT INTO loghistory (admin_id, fullname, access, date, time, event) VALUES (?, ?, ?, ?, ?, ?)";
                    cmd1.Parameters.Add("@admin_id", OdbcType.VarChar).Value = lblAdminID4.Text;
                    cmd1.Parameters.Add("@fullname", OdbcType.VarChar).Value = lblCurrent4.Text;
                    cmd1.Parameters.Add("@access", OdbcType.VarChar).Value = "OSAS";
                    cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                    cmd1.Parameters.Add("@time", OdbcType.VarChar).Value = lblTime.Text;
                    cmd1.Parameters.Add("@event", OdbcType.VarChar).Value = "LOGOUT";
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    logout();
                    this.Hide();
                    lblCurrent4.Text = "";
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {

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
                cmd1.CommandText = "UPDATE accounts SET status = 'OFFLINE' WHERE admin_id = '" + lblAdminID4.Text + "'";
                cmd1.ExecuteNonQuery();
                con.Close();

                this.Hide();
                Login call = new Login();
                call.Show();
                lblCurrent4.Text = "";
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        
        //TIME AND DATE
        private void ItsTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToString("MM-dd-yyyy");
        }

        //FORM LOAD
        private void Visitor_Load(object sender, EventArgs e)
        {
            notif.ForeColor = System.Drawing.Color.Green;
            txtScan3.Clear();
            txtScan3.Focus();
            display();
        }

        //DISPLAY VISITOR LOGS
        public void display()
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand("SELECT visitor_id as 'VISITOR ID', fname as 'First Name', lname as 'Last Name', plate_num as 'PLATE NUMBER', time_in as 'TIME IN', date as 'DATE' FROM visitor_archive ORDER BY time_in DESC;", con);
                OdbcDataAdapter adptr = new OdbcDataAdapter(cmd);
                DataSet ds = new DataSet();
                adptr.Fill(ds, "Empty");
                dgvVisitor.DataSource = ds.Tables[0];
                
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        //TEXTCHANGE ON SCANNING
        private void txtScan3_TextChanged(object sender, EventArgs e)
        {
            if (txtScan3.Text == XPlatenum)
            {
                try
                {
                    OdbcCommand cmdd = new OdbcCommand("SELECT * FROM visitor_archive WHERE id > 0 ORDER BY id DESC;", con);
                    OdbcDataAdapter adptrr = new OdbcDataAdapter(cmdd);
                    DataTable dtt = new DataTable();
                    adptrr.Fill(dtt);
                    
                    holder_X = dtt.Rows[0][1].ToString();
                    holder_id = dtt.Rows[0][0].ToString();
                    con.Close();

                    timer.Start();
                    txtScan3.Clear();
                    txtScan3.Focus();
                    notif.ForeColor = System.Drawing.Color.Green;
                    notif.Text = "QR Scanner Ready to Use.";
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else 
            {
                try
                {
                    checker();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("Data Truncated");

                }
           
                if (txtScan3.TextLength > 1)
                {
                    notif.ForeColor = Color.Red;
                    notif.Text = "Please wait for a few seconds.";
                }

                else if (txtScan3.Text == "")
                {
                    notif.ForeColor = System.Drawing.Color.Green;
                    notif.Text = "QR Scanner Ready to Use.";
                }
            }
        }

        //CHECKER VISITOR PASS AVAILABILITY
        private void checker()
        {
            try
            {
                if (txtScan3.Text.Length == 4)
                {
                    OdbcCommand cmddz = new OdbcCommand("SELECT * FROM visitor_avail WHERE avail_Vnum LIKE '" + txtScan3.Text + "';", con);
                    OdbcDataAdapter adptrrz = new OdbcDataAdapter(cmddz);
                    DataTable dttz = new DataTable();
                    adptrrz.Fill(dttz);
                    string i;

                    i = dttz.Rows[0][2].ToString();

                    if (i == "1")
                    {
                        insert();
                    }
                    else
                    {
                        exit();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Invalid Data", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                VisMSG1invalid show = new VisMSG1invalid();
                show.ShowDialog();
                txtScan3.Clear();
                txtScan3.Focus();
                if (txtScan3.Text == "")
                {
                    notif.ForeColor = System.Drawing.Color.Green;
                    notif.Text = "QR Scanner Ready to Use.";
                }
            }
        }

        //INSERT VISITOR DATA TO VISITOR LOGS
        private void insert()
        {
            try
            {
                con.Open();
                OdbcCommand cmd = new OdbcCommand();
                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO visitor_logs(visitor_id, fname, lname, type, plate_num, time_in, date, status)VALUES(?,?,?,?,?,?,?,?);";
                cmd.Parameters.Add("@visitor_id", OdbcType.VarChar).Value = txtScan3.Text;
                cmd.Parameters.Add("@fname", OdbcType.VarChar).Value = "Not Updated";
                cmd.Parameters.Add("@lname", OdbcType.VarChar).Value = "Not Updated";
                cmd.Parameters.Add("@type", OdbcType.VarChar).Value = "Not Updated";
                cmd.Parameters.Add("@plate_num", OdbcType.VarChar).Value = "Not Updated";
                cmd.Parameters.Add("@time_in", OdbcType.VarChar).Value = DateTime.Now.ToString("hh:mm:ss");
                cmd.Parameters.Add("@date", OdbcType.VarChar).Value = DateTime.Now.ToString("MM-dd-yyyy");
                cmd.Parameters.Add("@status", OdbcType.VarChar).Value = "Not Updated";

                if (cmd.ExecuteNonQuery() == 1)
                {
                   // DialogResult ask = MessageBox.Show("Visitor Now Entering!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //if (ask == DialogResult.OK)
                    //{
                    VisMSG3entry show = new VisMSG3entry();
                    show.ShowDialog();
                        con.Close();
                        archive();
                        unavail();
                        overall_inside();
                    //}
                }
                display();

                txtScan3.Clear();
                txtScan3.Focus();

                notif.ForeColor = System.Drawing.Color.Green;
                notif.Text = "QR Scanner Ready to Use.";
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }

        //INSERT INTO OVERALL INSIDE
        public void overall_inside()
        {
            try
            {
                con.Open();
                OdbcCommand cmd1 = new OdbcCommand();
                cmd1 = con.CreateCommand();
                cmd1.CommandText = "INSERT INTO overall_inside(class, type, date, entry_id) VALUES (?, ?, ?, ?)";
                cmd1.Parameters.Add("@class", OdbcType.VarChar).Value = "Visitor";
                cmd1.Parameters.Add("@type", OdbcType.VarChar).Value = "Not Updated";
                cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                cmd1.Parameters.Add("@entry_id", OdbcType.VarChar).Value = txtScan3.Text;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                //MessageBox.Show(ex.Message);
            }
        }

        //VISITOR EXIT
        public void exit()
        {
            OdbcCommand cmdd = new OdbcCommand("SELECT status FROM visitor_archive WHERE visitor_id = '" + txtScan3.Text + "';", con);
            OdbcDataAdapter adptrr = new OdbcDataAdapter(cmdd);
            DataTable dtt = new DataTable();
            adptrr.Fill(dtt);

            holds = dtt.Rows[0][0].ToString();
            if (holds == "Filled")
            {
                VisMSG4exit show = new VisMSG4exit();
                lblAns.Text = show.lblChoice.Text;

                show.ShowDialog();


                if (show.lblChoice.Text == "OK")
                //if (ask == DialogResult.OK)
                {
                    exitvalid();
                    update();
                    delete();
                    delete_overall();
                    avail();
                    Archive_Insert_Final();
                    display();
                    txtScan3.Clear();
                    txtScan3.Focus();
                }
                else if (show.lblChoice.Text == "CANCEL")
                {
                    //MessageBox.Show("Please Update quickly!!!!", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VisMSG5cancelexit show2 = new VisMSG5cancelexit();
                    show2.ShowDialog();
                    display();
                    txtScan3.Clear();
                    txtScan3.Focus();
                }

                con.Close();
            }
            else
            {
                //MessageBox.Show("The Visitor Haven't Filled Up Yet", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Unavailable Visitor Pass", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                VisMSG2empty show = new VisMSG2empty();
                show.ShowDialog();

                txtScan3.Clear();
                txtScan3.Focus();
            }
        }

        //INSERT TO OVERALL MONITORING
        private void overVisitor()
        {
            try
            {
                
                OdbcCommand cmdds = new OdbcCommand("SELECT type , plate_num FROM visitor_archive WHERE visitor_id = '" + txtScan3.Text + "' AND status = 'Filled';", con);
                OdbcDataAdapter adptrrss = new OdbcDataAdapter(cmdds);
                DataTable dttss = new DataTable();
                adptrrss.Fill(dttss);


                con.Open();
                OdbcCommand cmd1 = new OdbcCommand();
                cmd1 = con.CreateCommand();
                cmd1.CommandText = "INSERT INTO overall_monitoring (class, type, plate_num, date) VALUES (?, ?, ?, ?)";
                cmd1.Parameters.Add("@class", OdbcType.VarChar).Value = "Visitor";
                cmd1.Parameters.Add("@type", OdbcType.VarChar).Value = dttss.Rows[0][0].ToString();
                cmd1.Parameters.Add("@plate_num", OdbcType.VarChar).Value = dttss.Rows[0][1].ToString();
                cmd1.Parameters.Add("@date", OdbcType.VarChar).Value = lblDate.Text;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Can't fetch the Overall Visitor!!!");
            }
        }

        //SELECT VISITOR WERE STATUS IS FILLED
        public void exitvalid() {
            try
            {
                if (holds == "Filled")
                {
                    OdbcCommand cmddss = new OdbcCommand("SELECT `id`,`visitor_id`,`status` FROM visitor_archive WHERE id > 0 AND status = 'Filled' AND visitor_id = '"+ txtScan3.Text +"' ORDER BY id DESC;", con);
                    OdbcDataAdapter adptrrrr = new OdbcDataAdapter(cmddss);
                    DataTable dttt = new DataTable();
                    adptrrrr.Fill(dttt);

                    holder_X = dttt.Rows[0][1].ToString();
                    holder_id = dttt.Rows[0][0].ToString();
                    label3.Text = holder_X;
                    timer.Start();
                    txtScan3.Focus();

                    notif.ForeColor = System.Drawing.Color.Green;
                    notif.Text = "QR Scanner Ready to Use.";
                    con.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The status is not Filled yet", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //SAVE DATA TO VISITOR ARCHIVE
        private void archive()
        {
            try
            {
                con.Open();
                OdbcCommand cmds = new OdbcCommand();
                cmds = con.CreateCommand();
                cmds.CommandText = "INSERT INTO visitor_archive (visitor_id, fname, lname, type, plate_num, time_in, time_out, date, status)VALUES(?,?,?,?,?,?,?,?,?);";
                cmds.Parameters.Add("@visitor_id", OdbcType.VarChar).Value = txtScan3.Text;
                cmds.Parameters.Add("@fname", OdbcType.VarChar).Value = "Not Updated";
                cmds.Parameters.Add("@lname", OdbcType.VarChar).Value = "Not Updated";
                cmds.Parameters.Add("@type", OdbcType.VarChar).Value = "Not Updated";
                cmds.Parameters.Add("@plate_num", OdbcType.VarChar).Value = "Not Updated";
                cmds.Parameters.Add("@time_in", OdbcType.VarChar).Value = DateTime.Now.ToString("hh:mm:ss");
                cmds.Parameters.Add("@time_out", OdbcType.VarChar).Value = "Not Updated";
                cmds.Parameters.Add("@date", OdbcType.VarChar).Value = DateTime.Now.ToString("MM-dd-yyyy");
                cmds.Parameters.Add("@status", OdbcType.VarChar).Value = "Not Updated";

                if (cmds.ExecuteNonQuery() == 1)
                {

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        //UPDATE SET UNAVAILABLE
        public void unavail()
        {
            con.Open();
            OdbcCommand cmd = new OdbcCommand();
            cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE visitor_avail SET avail_status = '0' WHERE avail_Vnum =  '" + txtScan3.Text + "';";
            if (cmd.ExecuteNonQuery() == 1)
            {

            }
            con.Close();
        }

        //UPDATE VISITOR DATA ON VISITOR ARCHIVE
        public void update()
        {
            con.Open();
            OdbcCommand cmd = new OdbcCommand();
            cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE visitor_archive SET time_out = '" + lblTime.Text +"' WHERE visitor_id =  '" + txtScan3.Text + "';";
            if (cmd.ExecuteNonQuery() == 1)
            {

            }
            con.Close();
        }

        //UPDATE SET AVAILABLE
        public void avail()
        {
            con.Open();
            OdbcCommand cmd = new OdbcCommand();
            cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE visitor_avail SET avail_status = '1' WHERE avail_Vnum =  '" + txtScan3.Text + "';";
            if (cmd.ExecuteNonQuery() == 1)
            {

            }
            con.Close();
        }

        //DELETE VISITOR DATA FROM VISITOR LOGS
        public void delete()
        {
            con.Open();
            OdbcCommand cmds = new OdbcCommand();
            cmds = con.CreateCommand();
            cmds.CommandText = "DELETE FROM visitor_logs WHERE visitor_id = '" + txtScan3.Text + "'";
            cmds.ExecuteNonQuery();

            if (cmds.ExecuteNonQuery() == 1)
            {

            }
            con.Close();
        }

        //DELETE VISITOR DATA FROM OVERALL INSIDE
        public void delete_overall()
        {
            con.Open();
            OdbcCommand cmds = new OdbcCommand();
            cmds = con.CreateCommand();
            cmds.CommandText = "DELETE FROM overall_inside WHERE entry_id = '" + txtScan3.Text + "'";
            cmds.ExecuteNonQuery();

            if (cmds.ExecuteNonQuery() == 1)
            {

            }
            con.Close();
        }

        //SPECIAL CHARACTERS VALIDATION
        private void txtScan3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                //MessageBox.Show("Invalid Characters", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtScan3.Clear();
                txtScan3.Focus();
            }
        }

        //MOVING ARCHIVE TO ARCHIVE FINAL TIMER
        int m = 0, s = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            s++;

            if (s>60) 
            {
                m++;
                s = 0;
            }

            if (m > 1) 
            {
                m++;
                m = 0;
                s = 0;
                timer.Stop();
                
                
            }
            lblMin.Text = m.ToString();
            lblSec.Text = s.ToString();
        }
        
        //DELETE VISITOR DATA ON ARCHIVE
        public void Archieve_Delete() {

            try {
                
                OdbcCommand cmds = new OdbcCommand();
                cmds = con.CreateCommand();
                cmds.CommandText = "DELETE FROM visitor_archive WHERE visitor_id = '" + holder_X + "' AND status = 'Filled'";
                cmds.ExecuteNonQuery();

                if (cmds.ExecuteNonQuery() == 1)
                {

                }
                
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        
        }

        //INSERT VISITOR DATA FROM  ARCHIVE TO ARCHIVE FINAL
        public void Archive_Insert_Final() {
            try {
                con.Open();
                OdbcCommand cmddss = new OdbcCommand("SELECT * FROM visitor_archive WHERE visitor_id = '" + holder_X + "' AND status = 'Filled'", con);
                OdbcDataAdapter adptrrs = new OdbcDataAdapter(cmddss);
                DataTable dtts = new DataTable();
                adptrrs.Fill(dtts);
                if (cmddss.ExecuteNonQuery() == 1) 
                {
                    con.Close();
                }

                con.Open();
                OdbcCommand cmds = new OdbcCommand();
                cmds = con.CreateCommand();
                cmds.CommandText = "INSERT INTO visitor_archive_Final (id,visitor_id, fname, lname, type, plate_num, time_in, time_out, date, status)VALUES(?,?,?,?,?,?,?,?,?,?);";
                cmds.Parameters.Add("@id", OdbcType.VarChar).Value = dtts.Rows[0][0].ToString();
                cmds.Parameters.Add("@visitor_id", OdbcType.VarChar).Value = dtts.Rows[0][1].ToString();
                cmds.Parameters.Add("@fname", OdbcType.VarChar).Value = dtts.Rows[0][2].ToString();
                cmds.Parameters.Add("@lname", OdbcType.VarChar).Value = dtts.Rows[0][3].ToString();
                cmds.Parameters.Add("@type", OdbcType.VarChar).Value = dtts.Rows[0][4].ToString();
                cmds.Parameters.Add("@plate_num", OdbcType.VarChar).Value = dtts.Rows[0][5].ToString();
                cmds.Parameters.Add("@time_in", OdbcType.VarChar).Value = dtts.Rows[0][6].ToString();
                cmds.Parameters.Add("@time_out", OdbcType.VarChar).Value = dtts.Rows[0][7].ToString();
                cmds.Parameters.Add("@date", OdbcType.VarChar).Value = dtts.Rows[0][8].ToString();
                cmds.Parameters.Add("@status", OdbcType.VarChar).Value = dtts.Rows[0][9].ToString();
                if (cmds.ExecuteNonQuery() == 1)
                {
                    Archieve_Delete();
                }
                con.Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error Archieve Insert Final", "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.Message);
                con.Close();
            }        
        }

    }
}
