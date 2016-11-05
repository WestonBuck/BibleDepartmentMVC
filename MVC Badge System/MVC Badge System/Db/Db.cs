using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Db
{
    public class Db
    {
        public const string Connection = "Data Source=.\\SQLEXPRESS;Initial Catalog=GSTdata;Integrated Security=True;" +
                                         "Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;" +
                                         "MultiSubnetFailover=False";

        public static void CreateGift(Gift gift)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("INSERT INTO GIFT(badge_id, sender_id, recippient_id, tree_loc_x, tree_loc_y, comment)" +
                           "VALUES (@BadgeId, @SenderId, @RecipientId, @TreeLocX, @TreeLocY, @Comment)",
                           new
                           {
                               BadgeId = gift.BadgeId,
                               SenderId = gift.SenderId,
                               RecipientId = gift.RecipientId,
                               TreeLocX = gift.TreeLocX,
                               TreeLocY = gift.TreeLocY,
                               Comment = gift.Comment
                           });
            }
        }

        public static void UpdateGift(Gift gift)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("UPDATE GIFT SET badge_id = @BadgeId, sender_id = @SenderId, recipient_id = @RecipientId " +
                           "tree_loc_x = @TreeLocX, tree_loc_y = @TreeLocY, comment = @Comment " +
                           "WHERE gift_id = @GiftId",
                           new
                           {
                               BadgeId = gift.BadgeId,
                               SenderId = gift.SenderId,
                               RecipientId = gift.RecipientId,
                               TreeLocX = gift.TreeLocX,
                               TreeLocY = gift.TreeLocY,
                               Comment = gift.Comment,
                               GiftId = gift.GiftId
                           });
            }
        }

        public static Gift GetGift(int giftId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.QueryFirstOrDefault<Gift>("SELECT * FROM GIFT g WHERE g.gift_id = @GiftId",
                    new
                    {
                        GiftId = giftId
                    });
            }
        }

        public static IEnumerable<Gift> GetAllGifts()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.Query<Gift>("SELECT * FROM GIFT");
            }
        }

        public static void DeleteGift(Gift gift)
        {
            DeleteGift(gift.GiftId);
        }

        public static void DeleteGift(int giftId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM GIFT WHERE gift_id = @GiftId",
                    new
                    {
                        GiftId = giftId
                    });
            }
        }

        public static void CreateBadge(Badge b)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = @"INSERT INTO BADGES(badge_type, begin_date, retirement_date, name, self_give, student_give, staff_give, faculty_give)" +
                    "VALUES('@type','@startDate','@retireDate','@name','@self','@student','@staff','@faculty');";

                conn.Query<Badge>(sql,
                    new
                    {
                        type = b.Type,
                        startDate = b.BeginDate,
                        retireDate = b.RetirementDate,
                        name = b.Name,
                        self = b.SelfGive,
                        student = b.StudentGive,
                        staff = b.StaffGive,
                        faculty = b.FacultyGive,
                    });
            }
        }

        public static void UpdateBadge(Badge b)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("UPDATE BADGES SET badge_type = @Type, begin_date = @startDate, retirement_date = @retireDate " +
                           "name = @name, self_give = @self, staff_give = @staff, student_give = @student, faculty_give = @faculty" +
                           "WHERE badge_id = @id",
                           new
                           {
                               id = b.BadgeId,
                               type = b.Type,
                               startDate = b.BeginDate,
                               retireDate = b.RetirementDate,
                               name = b.Name,
                               self = b.SelfGive,
                               student = b.StudentGive,
                               staff = b.StaffGive,
                               faculty = b.FacultyGive,
                           });
            }
        }

        public static Badge GetBadge(int badgeId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.QueryFirstOrDefault<Badge>("SELECT * FROM BADGES WHERE badge_id = @BadgeId",
                    new{ BadgeId = badgeId}
                    );
            }
        }

        public static List<Badge> GetAllBadges()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.Query<Badge>("SELECT * FROM BADGES").AsList();
            }
        }

        public static void DeleteBadge(Badge badge)
        {
            DeleteBadge(badge.BadgeId);
        }

        public static void DeleteBadge(int badgeId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM BADGES WHERE badge_id = @BadgeId",
                    new
                    {
                        BadgeId = badgeId
                    });
            }
        }

        public static void CreateUser(User u)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = @"INSERT INTO USERS VALUES('@id','@fname','@lname','@email','@photoURL','@type','@shareLink');";

                conn.Query<User>(sql,
                    new
                    {
                        id = u.UserId,
                        fname = u.FirstName,
                        lname = u.LastName,
                        email = u.Email,
                        photoURL = u.PhotoUrl,
                        type = u.Type,
                        shareLink = u.ShareableLink
                    });
            }
        }
    }
}