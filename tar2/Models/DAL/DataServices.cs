using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using tar2.Models;
using tar2.Models.DAL;


public class DataServices
{
    public SqlDataAdapter da;
    public DataTable dt;
    public DataServices()
    {
      
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //---------------------------------------------------------------------------------
    //this method inserts the episode to db and also as favorite for the given user id.//
    //---------------------------------------------------------------------------------
    public int Insert(Episode episode,int id)
    {

        SqlConnection con;
        SqlCommand cmd,cmd2;
        con = connect("DBConnectionString"); // create the connection

        //this try is for inserting the data to the episods table in db
        try  
        {
            String cStr = BuildInsertCommand(episode);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
            }
            else throw;
        }

        //this try is for inserting the data to the favorites table in db for each user
        try { 
            String cStr2 = BuildInsertCommandFavorite(episode,id);      // helper method to build the insert string
            cmd2 = CreateCommand(cStr2, con);
            int numEffected=cmd2.ExecuteNonQuery();
            return numEffected;
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627 )
            {
                return 0;
            }
            else throw;
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    //this method inserts the series to the db.//
    //---------------------------------------------------------------------------------
    public int Insert(Series series)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommand(series);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (SqlException ex)
        {
            //if the series already exist we add 1 for the counter of likes in the db
            if (ex.Number == 2627)
            {
                return 0;
            }
            else throw;
        }
     

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert command String to the favotrits table
    //--------------------------------------------------------------------
    private String BuildInsertCommandFavorite(Episode episode,int id)
    {
        String command;
        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}','{2}')", episode.Id,id,episode.SeriesId);
        String prefix = "INSERT INTO Favorite_2021 " + "([Eid], [userid],[seriesid])";
        command = prefix + sb.ToString();

        return command;
    }

    //--------------------------------------------------------------------
    // Build the Insert command String for the episodes table
    //--------------------------------------------------------------------
    private String BuildInsertCommand(Episode episode)
    {
        String command;
        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", episode.Id, episode.SeriesId, episode.Name, episode.NameOfEp, episode.Picture, 
            episode.Description, episode.NumOfSeason, episode.Date);
        String prefix = "INSERT INTO Episodes_2021 " + "([id], [seriesId],[name],[nameOfEp],[picture],[description],[numOfSeason],[date])";
        command = prefix + sb.ToString();

        return command;
    }

    //--------------------------------------------------------------------
    // Build the Insert command String for the serires table
    //--------------------------------------------------------------------
    private String BuildInsertCommand(Series series)
    {
        String command;
        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", series.Id, series.First_air_date, series.Name,series.Origin_country, series.Original_language, 
            series.Overview, series.Popularity, series.Poster_path);
        String prefix = "INSERT INTO Series_2021 " + "([id], [first_air_date],[name],[origin_country],[original_language],[overview],[popularity],[poster_path])";
        command = prefix + sb.ToString();

        return command;
    }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }


    //---------------------------------------------------------------------------------
    // this is for inserting the users to the db
    //---------------------------------------------------------------------------------
    public int Insert(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("DBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildInsertCommand(user);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
                return 0;
            }
            else throw;
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
       
    }

    //--------------------------------------------------------------------
    // Build the Insert command String to users table
    //--------------------------------------------------------------------
    private String BuildInsertCommand(User user)
    {
        String command;
        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')", user.Name, user.LastName, user.Mail, user.Password, 
            user.PhoneNum, user.Sex, user.YearOfBirth, user.FavGenre, user.Address);
        String prefix = "INSERT INTO Users_2021 " + "([name],[lastName],[mail],[password],[phoneNum],[gender],[yearOfBirth]," +
            "[favGenre],[address])";
        command = prefix + sb.ToString();

        return command;
    }


    //---------------------------------------------------------------------------------
    //this method inserting all the user data from the login form to the users table//
    //---------------------------------------------------------------------------------
    public User Get(String user, String pass)
    {
        SqlConnection con = null;
        User u = new User();
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Users_2021 as u WHERE u.mail ='"+user+"'AND u.password = '"+pass+"'" ;
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while(dr.Read())
            {   // Read till the end of the data into a row
                u.Name = (string)dr["name"];
                u.LastName = (string)(dr["lastName"]);
                u.Mail = user;
                u.Password = pass;
                u.PhoneNum = (string)(dr["phoneNum"]);
                u.Sex = (string)(dr["gender"]);
                u.YearOfBirth = (string)(dr["yearOfBirth"]);
                u.FavGenre = (string)(dr["favGenre"]);
                u.Address = (string)(dr["address"]);
                u.Id = (int)(dr["id"]);
                u.IsAdmin = (bool)(dr["isAdmin"]);

            }
            if (u.Name == null)
            {
                return null;
            }
            return u;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    //---------------------------------------------------------------------------------
    //this method gets episodes from the db, calls the GetEpisodesLikes helper function to retrieve the amount of likes for each episode.//
    //---------------------------------------------------------------------------------
    public List<Episode> GetEpisodes()
    {
        SqlConnection con = null;
        List<Episode> eList = new List<Episode>();
        IDictionary<int, int> episodesLikes = GetEpisodesLikes();
        Episode e;
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Episodes_2021";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                e = new Episode();
                e.Id = (int)dr["id"];
                e.SeriesId = (int)dr["seriesId"];
                e.Name = (string)dr["name"];
                e.NameOfEp = (string)dr["nameOfEp"];
                e.NumOfSeason = (int)dr["numOfSeason"];
                e.Date = (string)dr["date"];
                e.LikeCount = episodesLikes[e.Id];
                eList.Add(e);
            }
            return eList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    //---------------------------------------------------------------------------------
    //this is a helper method, to get the likes for each episode in the db.//
    //---------------------------------------------------------------------------------
    public IDictionary<int,int> GetEpisodesLikes()
    {
        SqlConnection con = null;
        IDictionary<int, int> episodesLikes = new Dictionary<int, int>();
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT  Eid,COUNT(*) as 'num' FROM  Favorite_2021 GROUP BY  Eid";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                episodesLikes.Add((int)dr["Eid"], (int)dr["num"]);

            }
            return episodesLikes;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    //---------------------------------------------------------------------------------
    //this method gets the favorite episodes for the given user.//
    //---------------------------------------------------------------------------------
    public List<Episode> Get(int id)
    {
        SqlConnection con = null;
        List<Episode> eList = new List<Episode>();
        Episode e;
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Episodes_2021 as e inner join Favorite_2021 as f on e.id = f.Eid AND f.userid=" + "'"+id+"'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                e = new Episode();
                e.Id = (int)dr["id"];
                e.SeriesId = (int)dr["seriesId"];
                e.Name = (string)dr["name"];
                e.NameOfEp = (string)dr["nameOfEp"];
                e.Picture = (string)dr["picture"];
                e.Description = (string)dr["description"];
                e.NumOfSeason = (int)dr["numOfSeason"];
                e.Date = (string)dr["date"];
                eList.Add(e);
            }
            return eList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    //---------------------------------------------------------------------------------
    //this method gets the episodes for the given series, that is favorite for the user with the given id.//
    //---------------------------------------------------------------------------------
    public List<Episode> Get(String tvShow,int id)
    {
        SqlConnection con = null;
        List<Episode> eList = new List<Episode>();
        Episode e;
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Episodes_2021 as e inner join Favorite_2021 as f on e.id = f.Eid WHERE e.name ="+"'"+tvShow+"'"+ " AND f.userid=" + id;
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                e = new Episode();
                e.Id = (int)dr["id"];
                e.SeriesId = (int)dr["seriesId"];
                e.Name = (string)dr["name"];
                e.NameOfEp = (string)dr["nameOfEp"];
                e.Picture = (string)dr["picture"];
                e.Description = (string)dr["description"];
                e.NumOfSeason = (int)dr["numOfSeason"];
                e.Date = (string)dr["date"];
                eList.Add(e);
            }
            return eList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    //---------------------------------------------------------------------------------
    //this method gets the users info from the db.//
    //---------------------------------------------------------------------------------
    public List<User> Get()
    {
        SqlConnection con = null;
        List<User> elist = new List<User>();
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Users_2021";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                User u = new User();
                u.Name = (string)dr["name"];
                u.LastName = (string)(dr["lastName"]);
                u.Mail = (string)(dr["mail"]);
                u.Password = "Private";
                u.PhoneNum = (string)(dr["phoneNum"]);
                u.Sex = (string)(dr["gender"]);
                u.YearOfBirth = (string)(dr["yearOfBirth"]);
                u.FavGenre = (string)(dr["favGenre"]);
                u.Address = (string)(dr["address"]);
                u.Id = (int)(dr["id"]);
                u.IsAdmin = (bool)(dr["isAdmin"]);
                elist.Add(u);

            }
            return elist;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    //---------------------------------------------------------------------------------
    //this method gets the series info from the db.//
    //---------------------------------------------------------------------------------
    public List<Series> GetSeries()
    {
        SqlConnection con = null;
        List<Series> elist = new List<Series>();
        IDictionary<int, int> seriesLikes = GetSeriesLikes();
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM Series_2021";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            
            while (dr.Read())
            {   // Read till the end of the data into a row
                Series u = new Series();
                u.Id = (int)(dr["id"]);
                u.Name = (string)dr["name"];
                u.First_air_date = (string)(dr["first_air_date"]);
                u.Origin_country = (string)(dr["origin_country"]);
                u.Original_language = (string)(dr["original_language"]);
                u.Popularity = (string)(dr["popularity"]);
                u.Likes= seriesLikes[u.Id];
                elist.Add(u);

            }
            return elist;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    //---------------------------------------------------------------------------------
    //this is a helper method, to get the likes for each series in the db.//
    //---------------------------------------------------------------------------------
    public IDictionary<int, int> GetSeriesLikes()
    {

        SqlConnection con = null;
        IDictionary<int, int> seriesLikes = new Dictionary<int, int>();
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT seriesid, COUNT(DISTINCT userid) AS numRows FROM Favorite_2021 GROUP BY seriesid;";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                seriesLikes.Add((int)dr["seriesid"], (int)dr["numRows"]);

            }
            return seriesLikes;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    //---------------------------------------------------------------------------------
    //this method adds the given actor to the favorite actor table with the id of the user that liked him.//
    //---------------------------------------------------------------------------------
    public int Insert(Actor actor,int id)
    {
        SqlConnection con;
        SqlCommand cmd, cmd2;
        con = connect("DBConnectionString"); // create the connection

        //this try is for inserting the data to the episods table in db
        try
        {
            String cStr = BuildInsertCommand(actor);      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command
            cmd.ExecuteNonQuery(); // execute the command
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
            }
            else throw;
        }

        //this try is for inserting the data to the favorites table in db for each user
        try
        {
            String cStr2 = BuildInsertCommandFavoriteActor(actor,id);      // helper method to build the insert string
            cmd2 = CreateCommand(cStr2, con);
            int numEffected = cmd2.ExecuteNonQuery();
            return numEffected;
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
                return 0;
            }
            else throw;
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    //this method builds insert command to insert the given actor into db.//
    //---------------------------------------------------------------------------------
    private String BuildInsertCommand(Actor actor)
    {
        String command;
        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic  string
        sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}','{4}')", actor.Id, actor.Gender, actor.Name, actor.Popularity,actor.Picture);
        String prefix = "INSERT INTO Actors_2021 " + "([id],[gender],[name],[popularity],[picture])";
        command = prefix + sb.ToString();

        return command;
    }

    //---------------------------------------------------------------------------------
    //this method builds insert command to insert the given actor into db with the given user to favorite him.//
    //---------------------------------------------------------------------------------
    private String BuildInsertCommandFavoriteActor(Actor actor, int id)
    {
        String command;
        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic  string
        sb.AppendFormat("Values('{0}', '{1}')", actor.Id, id);
        String prefix = "INSERT INTO FavoriteActor_2021 " + "([Aid],[userid])";
        command = prefix + sb.ToString();

        return command;
    }

    //---------------------------------------------------------------------------------
    //this method gets all the favorite actors for the given user.//
    //---------------------------------------------------------------------------------
    public List<Actor> GetActors(int id)
    {
        SqlConnection con = null;
        List<Actor> eList = new List<Actor>();
        Actor e;
        try
        {
            con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "select * from FavoriteActor_2021 as f inner join Actors_2021 as a on f.Aid=a.id where userid=" + id;
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row

                e = new Actor();
                e.Id = (int)dr["id"];
                e.Gender = (int)dr["gender"];
                e.Name = (string)dr["name"];
                e.Popularity = (string)dr["popularity"];
                e.Picture= (string)dr["picture"];

                eList.Add(e);
            }
            return eList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

}