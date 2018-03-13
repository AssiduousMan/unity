using UnityEngine;  
using System;  
using System.Data;  
using System.Collections;   
using MySql.Data.MySqlClient;
using MySql.Data;
using System.IO;
public class SqlAccess :MonoBehaviour
{

    public static MySqlConnection dbConnection;
    //如果只是在本地的话，写localhost就可以。
    // static string host = "localhost";  
    //如果是局域网，那么写上本机的局域网IP
    static string host = "10.10.27.20";  
    //static string host = "10.10.27.57";
    //static string id = "wyx";
    static string id = "wyx";
    static string pwd = "woAIniniaiWO0121";
    //static string pwd = "123456";
    static string database = "unity";
    static string s = "1";
    static string s1 = "1";
    static string s2 = "1";
    static string s3 = "1";

    public SqlAccess()
    {
        OpenSql();
    }

    public static void OpenSql()
	{
	   
		try
		{
			string connectionString = string.Format("Server = {0};port={4};Database = {1}; User ID = {2}; Password = {3};",host,database,id,pwd,"3306");
			dbConnection = new MySqlConnection(connectionString);
			dbConnection.Open();
            s3 = "服务器连接成功";
            //if(s == null && s1 == null && s2 == null)
                //Debug.Log(1111111);
		}catch (Exception e)
		{
            if (s.Equals("1"))
                s = "服务器连接失败，请重新检查是否打开MySql服务。";
            else if(s1.Equals("1"))
                s1 = "服务器连接失败，请重新检查是否打开MySql服务。";
            else if(s2.Equals("1"))
                s2 = "服务器连接失败，请重新检查是否打开MySql服务。"; Debug.Log(e.Message.ToString());
            throw new Exception("服务器连接失败，请重新检查是否打开MySql服务。" + e.Message.ToString());
            

        }
		
	}

    public DataSet SelectToAccount(string sql) {

        return ExecuteQuery(sql);

    }

    public DataSet InsertToAccount(string sql) {

        return ExecuteQuery(sql);

    }
	
	public  void Close()
	{
		
		if(dbConnection != null)
		{
			dbConnection.Close();
			dbConnection.Dispose();
			dbConnection = null;
		}
		
	}
	
    public static DataSet ExecuteQuery(string sqlString)  
    {  
		if(dbConnection.State==ConnectionState.Open)
		{
     		DataSet ds = new DataSet();  
      		try  
	    	{  
	       
				MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection); 
				da.Fill(ds);
			
	    	}  
		    catch (Exception ee)  
		    {
		        throw new Exception("SQL:" + sqlString + "/n" + ee.Message.ToString());  
		    }
			finally
			{
			}
			return ds;
		}
	    return null;
	}

    public void OnGUI()
    {
        //GUILayout.Label(s);
        //GUILayout.Label(s1);
        //GUILayout.Label(s2);
        GUILayout.Label(s3);
    }

}
