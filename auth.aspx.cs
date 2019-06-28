using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace TimeTracker
{
    public partial class auth : System.Web.UI.Page
    {
        static public string login;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label3.Text = null; //for msg

            employeesDataContextDataContext db = new employeesDataContextDataContext();

            var get_log = (from a in db.employees where a.login == TextBox1.Text select a).FirstOrDefault(); //get the needed log from db
            int id;


            if (get_log != null)
            {
                id = get_log.id_employee;

                //login
                login = Convert.ToString(get_log.login);
                login = System.Text.RegularExpressions.Regex.Replace(login, @"\s+", ""); //remove spaces
                //psw
                string psw = Convert.ToString(get_log.pswd);
                psw = System.Text.RegularExpressions.Regex.Replace(psw, @"\s+", ""); //remove spaces
                //role
                string role = Convert.ToString(get_log.role);
                role = System.Text.RegularExpressions.Regex.Replace(role, @"\s+", ""); //remove spaces


                if (TextBox1.Text == login && TextBox2.Text == psw)
                {
                    if (role == "admin")
                    {
                        //Response.Redirect("/editEmplolyess.aspx");
                        Response.Redirect("editEmplolyess.aspx?id=" + Server.UrlEncode(id.ToString()));                    
                    }
                    else
                    {
                        //Response.Redirect("/workPage.aspx");
                        Response.Redirect("workPage.aspx?id=" + Server.UrlEncode(id.ToString()));
                    }                     
                    
                }
                else
                {
                    Label3.Text = "Неверный пароль или логин!!!";                    
                    Label3.CssClass = "msg msg-error";
                    TextBox1.Text = null;
                    TextBox2.Text = null;
                }
            }
            else
            {
                Label3.Text = "Данный пользователь не зарегистрирован в системе!";
                Label3.CssClass = "msg msg-error";
                TextBox1.Text = null;
                TextBox2.Text = null;
            }

        }
    }
}