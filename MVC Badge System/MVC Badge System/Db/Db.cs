﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Db
{
    public class Db
    {

        public static string Connection = "Data Source=.\\SQLEXPRESS;Initial Catalog=GSTdata;Integrated Security=True;" +
                                         "Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;" +
                                         "MultiSubnetFailover=False";

        //
        // GIFT
        //
        public static int CreateGift(Gift gift)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "INSERT INTO BADGE_GIFTS(gift_date, badge_id, sender_id, recipient_id, tree_loc_x, tree_loc_y, comment)" +
                             "VALUES (@GiftDate, @BadgeId, @SenderId, @RecipientId, @TreeLocX, @TreeLocY, @Comment); SELECT CAST(SCOPE_IDENTITY() as int)";

                return conn.Query<int>(sql, gift).Single();
            }
        }

        public static void UpdateGift(Gift gift)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql =
                    "UPDATE BADGE_GIFTS SET gift_date = @GiftDate, badge_id = @BadgeId, sender_id = @SenderId, recipient_id = @RecipientId, " +
                    "tree_loc_x = @TreeLocX, tree_loc_y = @TreeLocY, comment = @Comment " +
                    "WHERE gift_id = @GiftId";

                conn.Query(sql, gift);
            }
        }

        public static Gift GetGift(int? giftId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "SELECT gift_date GiftDate, gift_id GiftId, badge_id BadgeId, " +
                             "sender_id SenderId, recipient_id RecipientId, " +
                             "tree_loc_x TreeLocX, tree_loc_y TreeLocY," +
                             "comment Comment FROM BADGE_GIFTS WHERE gift_id = @GiftId";

                Gift gift = conn.QueryFirstOrDefault<Gift>(sql,
                    new
                    {
                        GiftId = giftId
                    });

                gift.Recipient = GetUser(gift.RecipientId);
                gift.Sender = GetUser(gift.SenderId);
                gift.BadgeGift = GetBadge(gift.BadgeId);

                return gift;
            }
        }

        public static List<Gift> GetGiftsGivenTo(int? userID)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Gift> giftList = conn.Query<Gift>("SELECT gift_date GiftDate, gift_id GiftId, badge_id BadgeId, " +
                                                       "sender_id SenderId, recipient_id RecipientId, " +
                                                       "tree_loc_x TreeLocX, tree_loc_y TreeLocY," +
                                                       "comment Comment FROM BADGE_GIFTS WHERE recipient_id = @uID",
                                                       new { uID = userID }).AsList();

                foreach (Gift g in giftList)
                {
                    g.Recipient = GetUser(g.RecipientId);
                    g.Sender = GetUser(g.SenderId);
                    g.BadgeGift = GetBadge(g.BadgeId);
                }

                return giftList;
            }
        }

        public static List<Gift> GetGiftsGivenTo(User user)
        {
            return GetGiftsGivenTo(user.UserId);
        }

        public static List<Gift> GetGiftsGivenTo(int? userID, int? badgeID)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Gift> giftList = conn.Query<Gift>("SELECT gift_date GiftDate, gift_id GiftId, badge_id BadgeId, " +
                                                       "sender_id SenderId, recipient_id RecipientId, " +
                                                       "tree_loc_x TreeLocX, tree_loc_y TreeLocY," +
                                                       "comment Comment FROM BADGE_GIFTS WHERE recipient_id = @uID and badge_id = @bID",
                                                       new
                                                       {
                                                           uID = userID,
                                                           bID = badgeID
                                                       }).AsList();

                foreach (Gift g in giftList)
                {
                    g.Recipient = GetUser(g.RecipientId);
                    g.Sender = GetUser(g.SenderId);
                    g.BadgeGift = GetBadge(g.BadgeId);
                }

                return giftList;
            }
        }

        public static List<Gift> GetGiftsGivenTo(int? userID, Badge badge)
        {
            return GetGiftsGivenTo(userID, badge.BadgeId);
        }

        public static List<Gift> GetGiftsGivenTo(User user, Badge badge)
        {
            return GetGiftsGivenTo(user.UserId, badge.BadgeId);
        }

        public static List<Gift> GetGiftsGivenTo(User user, int? badgeID)
        {
            return GetGiftsGivenTo(user.UserId, badgeID);
        }

        public static List<Gift> GetAllGifts()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Gift> giftList = conn.Query<Gift>("SELECT gift_date GiftDate, gift_id GiftId, badge_id BadgeId, " +
                                                       "sender_id SenderId, recipient_id RecipientId, " +
                                                       "tree_loc_x TreeLocX, tree_loc_y TreeLocY," +
                                                       "comment Comment FROM BADGE_GIFTS").AsList();

                foreach (Gift g in giftList)
                {
                    g.Recipient = GetUser(g.RecipientId);
                    g.Sender = GetUser(g.SenderId);
                    g.BadgeGift = GetBadge(g.BadgeId);
                }

                return giftList;
            }
        }

        public static void DeleteGift(Gift gift)
        {
            DeleteGift(gift.GiftId);
        }

        public static void DeleteGift(int? giftId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM BADGE_GIFTS WHERE gift_id = @GiftId",
                    new
                    {
                        GiftId = giftId
                    });
            }
        }


        //
        // BADGE
        //
        public static int CreateBadge(Badge b)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = @"INSERT INTO BADGES(descript, badge_type, begin_date, retirement_date, name, self_give, student_give, staff_give, faculty_give, picture)" +
                              "VALUES(@Description, @Type, @BeginDate, @RetirementDate, @Name, @SelfGive, @StudentGive, @StaffGive, @FacultyGive, @Picture); SELECT CAST(SCOPE_IDENTITY() as int)";
                
                return conn.Query<int>(sql, b).Single();
            }
        }

        public static void UpdateBadge(Badge b)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql =
                    "UPDATE BADGES SET descript = @Description, badge_type = @Type, begin_date = @BeginDate, retirement_date = @RetirementDate, " +
                    "name = @Name, self_give = @SelfGive, staff_give = @StaffGive, student_give = @StudentGive, faculty_give = @FacultyGive, " +
                    "picture = @Picture " +
                    "WHERE badge_id = @BadgeId";
                conn.Query(sql, b);
            }
        }

        public static Badge GetBadge(int? badgeId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                Badge b = conn.QueryFirstOrDefault<Badge>("SELECT badge_id BadgeId, descript Description, badge_type Type," +
                                                       "retirement_date RetirementDate, begin_date BeginDate," +
                                                       "name Name, self_give SelfGive, student_give StudentGive," +
                                                       "staff_give StaffGive, faculty_give FacultyGive, picture Picture FROM BADGES WHERE badge_id = @BId",
                    new { BId = badgeId }
                    );

                if (b != null && b.Type == BadgeType.Apple)
                {
                    b.Prerequisites = GetPrerequisites(b.BadgeId);
                }

                return b;
            }
        }

        public static List<Badge> GetBadges(BadgeType type)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "SELECT badge_id BadgeId, descript Description, badge_type Type, " +
                             "retirement_date RetirementDate, begin_date BeginDate, " +
                             "name Name, self_give SelfGive, student_give StudentGive, " +
                             "staff_give StaffGive, faculty_give FacultyGive, picture Picture FROM BADGES " +
                             "WHERE badge_type = @Type;";

                List<Badge> badgeList = conn.Query<Badge>(sql, new { Type = (int)type }).AsList();

                foreach (Badge b in badgeList)
                {
                    if (b.Type == BadgeType.Apple)
                    {
                        b.Prerequisites = GetPrerequisites(b.BadgeId);
                    }
                }

                return badgeList;
            }
        }

        public static List<Badge> GetBadges(User sender, User recipient)
        {
            if (recipient.UserType == UserType.Student)
            {
                string senderType;

                if (recipient.UserId == sender.UserId)
                {
                    senderType = "self";
                }
                else
                {
                    switch (sender.UserType)
                    {
                        case UserType.Student:
                            senderType = "student";
                            break;
                        case UserType.Staff:
                            senderType = "staff";
                            break;
                        case UserType.Faculty:
                            senderType = "faculty";
                            break;
                        default:
                            return GetAllBadges();

                    }
                }

                using (IDbConnection conn = new SqlConnection(Connection))
                {
                    string sql = "SELECT badge_id BadgeId, descript Description, badge_type Type, " +
                                 "retirement_date RetirementDate, begin_date BeginDate, " +
                                 "name Name, self_give SelfGive, student_give StudentGive, " +
                                 "staff_give StaffGive, faculty_give FacultyGive FROM BADGES " +
                                 "WHERE "+ senderType + "_give = 1 and retirement_date > GETDATE() and begin_date < GETDATE();";

                    List<Badge> badgeList = conn.Query<Badge>(sql).AsList();

                    foreach (Badge b in badgeList)
                    {
                        if (b.Type == BadgeType.Apple)
                        {
                            b.Prerequisites = GetPrerequisites(b.BadgeId);
                        }
                    }

                    return badgeList;
                }
            }
            else
            {
                //cannot send badges to those who are not students
                return null;
            }
        }

        public static List<Badge> GetBadges(User sender)
        {
            User recipient = new User();
            recipient.UserType = UserType.Student;
            recipient.UserId = -1;
            return GetBadges(sender, recipient);
        }

        public static List<Badge> GetAllBadges()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Badge> badgeList = conn.Query<Badge>("SELECT badge_id BadgeId, descript Description, badge_type Type," +
                                         "retirement_date RetirementDate, begin_date BeginDate," +
                                         "name Name, self_give SelfGive, student_give StudentGive," +
                                         "staff_give StaffGive, faculty_give FacultyGive, picture Picture FROM BADGES").AsList();
                foreach (Badge b in badgeList)
                {
                    if (b.Type == BadgeType.Apple)
                    {
                        b.Prerequisites = GetPrerequisites(b.BadgeId);
                    }
                }

                return badgeList;
            }
        }

        public static void DeleteBadge(Badge badge)
        {
            DeleteBadge(badge.BadgeId);
        }

        public static void DeleteBadge(int? badgeId)
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

        //
        // USER
        //
        public static int CreateUser(User u)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = @"INSERT USERS (first_name, last_name, email, photo_url, user_type, shareable_link)" +
                              "VALUES(@FirstName, @LastName, @Email, @PhotoUrl, @UserType, @ShareableLink); SELECT CAST(SCOPE_IDENTITY() as int)";

                return conn.Query<int>(sql, u).Single();
            }
        }

        public static void UpdateUser(User user)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "UPDATE USERS SET first_name = @FirstName, last_name = @LastName, email = @Email, " +
                             "photo_url = @PhotoUrl, user_type = @UserType, shareable_link = @ShareableLink " +
                             "WHERE user_id = @UserId";
                conn.Query(sql, user);
            }
        }

        public static User GetUser(int? userId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.QueryFirstOrDefault<User>("SELECT user_id UserId, first_name FirstName," +
                                                      "last_name LastName, email Email, photo_url PhotoUrl," +
                                                      "user_type UserType, shareable_link ShareableLink FROM USERS u WHERE user_id = @UserId",
                    new
                    {
                        UserId = userId
                    });
            }
        }

        public static List<User> GetUsersSearch(string searchTerm, UserType? type)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                searchTerm = "%" + searchTerm + "%";
                string typeQuery = (type.HasValue) ? " AND user_type = @Type" : "";
                return conn.Query<User>("SELECT user_id UserId, first_name FirstName," +
                                        "last_name LastName, email Email, photo_url PhotoUrl," +
                                        "user_type UserType, shareable_link ShareableLink FROM USERS " +
                                        "WHERE last_name LIKE @Search or first_name LIKE @Search or email LIKE @Search" + typeQuery,
                                        new { Search = searchTerm, Type = type }).AsList();
            }
        }

        public static User GetUserFromShareableHash(string ShareableLink)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.QueryFirstOrDefault<User>("SELECT user_id UserId, first_name FirstName," +
                                                      "last_name LastName, email Email, photo_url PhotoUrl," +
                                                      "user_type UserType, shareable_link ShareableLink FROM USERS u WHERE shareable_link = @ShareableLink",
                    new
                    {
                        ShareableLink
                    });
            }
        }

        public static List<User> GetAllUsers()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.Query<User>("SELECT user_id UserId, first_name FirstName," +
                                        "last_name LastName, email Email, photo_url PhotoUrl," +
                                        "user_type UserType, shareable_link ShareableLink FROM USERS").AsList();
            }
        }

        public static void DeleteUser(User user)
        {
            DeleteUser(user.UserId);
        }

        public static void DeleteUser(int? userId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM USERS WHERE user_id = @UserId",
                    new
                    {
                        UserId = userId
                    });
            }
        }

        //
        // PREREQUISITES
        //
        public static void CreatePrerequisite(int? ParentId, int? ChildId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "INSERT INTO PREREQUISITE (parent_id, child_id)" +
                             "VALUES (@ParentId, @ChildId);";
                conn.Query(sql,
                    new
                    {
                        ParentId,
                        ChildId
                    });
            }
        }

        public static void CreatePrerequisite(Badge Parent, Badge Child)
        {
            CreatePrerequisite(Parent.BadgeId, Child.BadgeId);
        }

        public static void CreatePrerequisite(Prerequisite p)
        {
            CreatePrerequisite(p.ParentId, p.ChildId);
        }

        public static void UpdatePrerequisite(Prerequisite p)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "UPDATE PREREQUISITE SET parent_id = @ParentId, child_id = @ChildId " +
                             "WHERE prerequisite_id = @PrerequisiteId;";

                conn.Query(sql, p);
            }
        }

        public static Prerequisite GetPrerequisite(int? PrerequisiteId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "SELECT parent_id ParentId, child_id ChildId " +
                             "FROM PREREQUISITE WHERE prerequisite_id = @PrerequisiteId;";

                Prerequisite p = conn.Query<Prerequisite>(sql,
                    new
                    {
                        PrerequisiteId
                    }).FirstOrDefault();

                p.Parent = GetBadge(p.ParentId);
                p.Child = GetBadge(p.ChildId);

                return p;
            }
        }

        public static Badge GetParentOfPrerequisite(int? ChildId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "SELECT prerequisite_id PrerequisiteId, parent_id ParentId " +
                             "FROM PREREQUISITE WHERE child_id = @ChildId;";

                Prerequisite p = conn.Query<Prerequisite>(sql,
                    new
                    {
                        ChildId
                    }).FirstOrDefault();

                return GetBadge(p.ParentId);
            }
        }

        public static List<Badge> GetPrerequisites(int? ParentId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "SELECT prerequisite_id PrerequisiteId, child_id ChildId " +
                             "FROM PREREQUISITE WHERE parent_id = @ParentId;";

                List<Prerequisite> preList = conn.Query<Prerequisite>(sql,
                    new
                    {
                        ParentId
                    }).AsList();

                List<Badge> badgeList = new List<Badge>();
                foreach (Prerequisite p in preList)
                {
                    badgeList.Add(GetBadge(p.ChildId));
                }

                return badgeList;
            }
        }

        public static void DeletePrerequisite(int? PrerequisiteId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM PREREQUISITE WHERE prerequisite_id = @PrerequisiteId;",
                    new
                    {
                        PrerequisiteId
                    });
            }
        }

        //
        // DEFAULT BADGES
        //
        public static void CreateDefaultBadge(DefaultBadge badge)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "INSERT INTO DEFAULT_BADGES (badge_id, badge_type, tree_loc_x, tree_loc_y)" +
                             "VALUES (@BadgeId, @Type, @TreeLocX, @TreeLocY);";
                conn.Query(sql, badge);
            }
        }

        public static void UpdateDefaultBadge(DefaultBadge badge)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "UPDATE DEFAULT_BADGES SET tree_loc_x = @TreeLocX, tree_loc_y = @TreeLocY badge_type @Type " +
                             "WHERE badge_id = @BadgeId;";

                conn.Query(sql, badge);
            }
        }

        public static DefaultBadge GetDefaultBadge(int? BadgeId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "SELECT badge_id BadgeId, tree_loc_x TreeLocX, tree_loc_y TreeLocY, badge_type Type " +
                             "FROM DEAFULT_BADGES WHERE badge_id = @BadgeId;";

                DefaultBadge badge = conn.Query<DefaultBadge>(sql,
                    new
                    {
                        BadgeId
                    }).FirstOrDefault();

                return badge;
            }
        }

        public static List<DefaultBadge> GetAllDefaultBadges()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "SELECT badge_id BadgeId, tree_loc_x TreeLocX, tree_loc_y TreeLocY, badge_type Type " +
                             "FROM DEFAULT_BADGES;";

                return conn.Query<DefaultBadge>(sql).AsList();
            }
        }

        public static void DeleteDefaultBadge(int? BadgeId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM DEFAULT_BADGES WHERE badge_id = @BadgeId;",
                    new
                    {
                        BadgeId
                    });
            }
        }

        public static void DeletePrerequisite(int? ParentId, int? ChildId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM PREREQUISITE WHERE parent_id = @ParentId AND child_id = @ChildId;",
                    new
                    {
                        ParentId,
                        ChildId
                    });
            }
        }
    }
}