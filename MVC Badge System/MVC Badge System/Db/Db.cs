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
    }
}