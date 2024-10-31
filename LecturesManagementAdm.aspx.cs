﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace Eduspire3
{
    public partial class LecturesManagementAdm : System.Web.UI.Page
    {
        String mycon = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRecord();
            }
        }
        private void LoadRecord()
        {
            lbl.Text = "All uploaded lectures";
            SqlConnection con = new SqlConnection(mycon);
            SqlCommand comm = new SqlCommand("select Video_Table12.ID,Video_Table12.tut_ID,Video_Table12.Title,Video_Table12.VideoOnCourse,Video_Table12.Description,Video_Table12.VideoPath,Video_Table12.UploadedOn,RegisterTut_Table1.tut_EmailID from Video_Table12,RegisterTut_Table1 where Video_Table12.tut_ID = RegisterTut_Table1.tut_ID", con);
            SqlDataAdapter d = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            d.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        private Boolean CheckCourse()
        {
            Boolean Courseavailable = false;
            SqlConnection con = new SqlConnection(mycon);
            String query = "Select * from Video_Table12 where VideoOnCourse='" + txtCourse.Text + "'";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = com;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Courseavailable = true;
            }
            con.Close();
            return Courseavailable;
        }
        private Boolean CheckEmail()
        {
            Boolean Emailavailable = false;
            SqlConnection con = new SqlConnection(mycon);
            String query = "select Video_Table12.ID,Video_Table12.tut_ID,Video_Table12.Title,Video_Table12.VideoOnCourse,Video_Table12.Description,Video_Table12.VideoPath,Video_Table12.UploadedOn,RegisterTut_Table1.tut_EmailID from Video_Table12,RegisterTut_Table1 where Video_Table12.tut_ID = RegisterTut_Table1.tut_ID and tut_EmailID='" + txtEmailID.Text + "'";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = com;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Emailavailable = true;
            }
            con.Close();
            return Emailavailable;
        }
        private bool ValidEmail()
        {
            Boolean Email = false;
            Regex regEmail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            Match matchEmail = regEmail.Match(txtEmailID.Text);
            if (matchEmail.Success)
            {
                Email = true;
            }
            return Email;
        }
        private void ClrPrevErrors()
        {
            lblCourseError.Text = "";
            lblEmailError.Text = "";
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            ClrPrevErrors();
            if (txtCourse.Text != "")
            {
                if (CheckCourse() == false)
                {
                    lblCourseError.Text = "Entered Course name doesn't exist";
                }
                else
                {
                    ClrPrevErrors();
                    lbl.Text = txtCourse.Text + " lectures";
                    SqlConnection con = new SqlConnection(mycon);
                    SqlCommand comm = new SqlCommand("select Video_Table12.ID,Video_Table12.tut_ID,Video_Table12.Title,Video_Table12.VideoOnCourse,Video_Table12.Description,Video_Table12.VideoPath,Video_Table12.UploadedOn,RegisterTut_Table1.tut_EmailID from Video_Table12,RegisterTut_Table1 where Video_Table12.tut_ID = RegisterTut_Table1.tut_ID and VideoOnCourse='" + txtCourse.Text + "'", con);
                    SqlDataAdapter d = new SqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    d.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            if (txtEmailID.Text != "")
            {
                if (ValidEmail() == false)
                {
                    lblEmailError.Text = "Please enter valid Email";
                }
                else if (CheckEmail() == false)
                {
                    lblEmailError.Text = "Entered Email doesn't exist";
                }
                else
                {
                    ClrPrevErrors();
                    lbl.Text = txtEmailID.Text + " uploaded lectures";
                    SqlConnection con = new SqlConnection(mycon);
                    SqlCommand comm = new SqlCommand("select Video_Table12.ID,Video_Table12.tut_ID,Video_Table12.Title,Video_Table12.VideoOnCourse,Video_Table12.Description,Video_Table12.VideoPath,Video_Table12.UploadedOn,RegisterTut_Table1.tut_EmailID from Video_Table12,RegisterTut_Table1 where Video_Table12.tut_ID = RegisterTut_Table1.tut_ID and tut_EmailID='" + txtEmailID.Text + "'", con);
                    SqlDataAdapter d = new SqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    d.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }

            }
            if (txtEmailID.Text != "" && txtCourse.Text != "")
            {
                if (CheckEmail() == false)
                {
                    lblEmailError.Text = "Entered Email doesn't exist";
                }
                else if (CheckCourse() == false)
                {
                    lblCourseError.Text = "Entered course name doesn't exist";
                }
                else
                {
                    ClrPrevErrors();
                    lbl.Text = txtEmailID.Text + "'s " + txtCourse.Text + " Uploaded lectures";
                    SqlConnection con = new SqlConnection(mycon);
                    SqlCommand comm = new SqlCommand("select Video_Table12.ID,Video_Table12.tut_ID,Video_Table12.Title,Video_Table12.VideoOnCourse,Video_Table12.Description,Video_Table12.VideoPath,Video_Table12.UploadedOn,RegisterTut_Table1.tut_EmailID from Video_Table12,RegisterTut_Table1 where Video_Table12.tut_ID = RegisterTut_Table1.tut_ID and tut_EmailID='" + txtEmailID.Text + "' and Video_Table12.VideoOnCourse='" + txtCourse.Text + "'", con);
                    SqlDataAdapter d = new SqlDataAdapter(comm);
                    DataTable dt = new DataTable();
                    d.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            if (txtEmailID.Text == "" && txtCourse.Text == "")
            {
                Response.Write("<script>alert('Please enter Email ID or Course Name')</script>");
            }
        }

        protected void btnShowAllLec_Click(object sender, EventArgs e)
        {
            LoadRecord();
            ClrPrevErrors();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ClrPrevErrors();
            Label ID = GridView1.Rows[e.RowIndex].FindControl("Label1") as Label;
            String Delete = "Delete from Video_Table12 where ID=" + ID.Text;
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = Delete;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Successfully Deleted!')</script>");
            LoadRecord();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadRecord();
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }
}