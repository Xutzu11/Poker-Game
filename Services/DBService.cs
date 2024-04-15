﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbetBeclean.Services
{
    internal class DBService
    {
        private readonly SqlConnection _connection;

        public DBService(SqlConnection connection)
        {
            _connection = connection;
        }
        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
        private void ExecuteNonQuery(string procedureName, SqlParameter[] parameters)
        {
            using (var command = new SqlCommand(procedureName, _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUser(int id, string username, int currentFont, int currentTitle, int currentIcon, int currentTable, int chips, int stack, int streak, int handsPlayed, int level, DateTime lastLogin)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@username", SqlDbType.VarChar) { Value = username },
                new SqlParameter("@currentFont", SqlDbType.Int) { Value = currentFont },
                new SqlParameter("@currentTitle", SqlDbType.Int) { Value = currentTitle },
                new SqlParameter("@currentIcon", SqlDbType.Int) { Value = currentIcon },
                new SqlParameter("@currentTable", SqlDbType.Int) { Value = currentTable },
                new SqlParameter("@chips", SqlDbType.Int) { Value = chips },
                new SqlParameter("@stack", SqlDbType.Int) { Value = stack },
                new SqlParameter("@streak", SqlDbType.Int) { Value = streak },
                new SqlParameter("@handsPlayed", SqlDbType.Int) { Value = handsPlayed },
                new SqlParameter("@level", SqlDbType.Int) { Value = level },
                new SqlParameter("@lastLogin", SqlDbType.DateTime) { Value = lastLogin }
            };
            ExecuteNonQuery("updateUser", parameters);
        }

        public void UpdateUserFont(int id, int font)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@font", SqlDbType.Int) { Value = font }
            };
            ExecuteNonQuery("updateUserFont", parameters);
        }

        public void UpdateUserTitle(int id, int title)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@title", SqlDbType.Int) { Value = title }
            };
            ExecuteNonQuery("updateUserTitle", parameters);
        }

        public void UpdateUserIcon(int id, int icon)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@icon", SqlDbType.Int) { Value = icon }
            };
            ExecuteNonQuery("updateUserIcon", parameters);
        }

        public void UpdateUserChips(int id, int chips)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@chips", SqlDbType.Int) { Value = chips }
            };
            ExecuteNonQuery("updateUserChips", parameters);
        }

        public void UpdateUserStack(int id, int stack)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@stack", SqlDbType.Int) { Value = stack }
            };
            ExecuteNonQuery("updateUserStack", parameters);
        }

        public void UpdateUserStreak(int id, int streak)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@streak", SqlDbType.Int) { Value = streak }
            };
            ExecuteNonQuery("updateUserStreak", parameters);
        }

        public void UpdateUserHandsPlayed(int id, int handsPlayed)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@handsPlayed", SqlDbType.Int) { Value = handsPlayed }
            };
            ExecuteNonQuery("updateUserHandsPlayed", parameters);
        }

        public void UpdateUserLevel(int id, int level)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@level", SqlDbType.Int) { Value = level }
            };
            ExecuteNonQuery("updateUserLevel", parameters);
        }

        public void UpdateUserLastLogin(int id, DateTime lastLogin)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@lastLogin", SqlDbType.DateTime) { Value = lastLogin }
            };
            ExecuteNonQuery("updateUserLastLogin", parameters);
        }

        public void UpdateChallenge(int challengeId, string description, string rule, int amount, int reward)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@challenge_id", SqlDbType.Int) { Value = challengeId },
                new SqlParameter("@challenge_description", SqlDbType.VarChar, -1) { Value = description },
                new SqlParameter("@challenge_rule", SqlDbType.VarChar, -1) { Value = rule },
                new SqlParameter("@challenge_amount", SqlDbType.Int) { Value = amount },
                new SqlParameter("@challenge_reward", SqlDbType.Int) { Value = reward }
            };
            ExecuteNonQuery("updateChallenge", parameters);
        }

        public void UpdateFont(int fontId, string fontName, int fontPrice, string fontType)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@font_id", SqlDbType.Int) { Value = fontId },
                new SqlParameter("@font_name", SqlDbType.VarChar, 255) { Value = fontName },
                new SqlParameter("@font_price", SqlDbType.Int) { Value = fontPrice },
                new SqlParameter("@font_type", SqlDbType.VarChar, 255) { Value = fontType }
            };
            ExecuteNonQuery("updateFont", parameters);
        }

        public void UpdateIcon(int iconId, string iconName, int iconPrice, string iconPath)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@icon_id", SqlDbType.Int) { Value = iconId },
                new SqlParameter("@icon_name", SqlDbType.VarChar, 255) { Value = iconName },
                new SqlParameter("@icon_price", SqlDbType.Int) { Value = iconPrice },
                new SqlParameter("@icon_path", SqlDbType.VarChar, 255) { Value = iconPath }
            };
            ExecuteNonQuery("updateIcon", parameters);
        }

        public void UpdateTitle(int titleId, string titleName, int titlePrice, string titleContent)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@title_id", SqlDbType.Int) { Value = titleId },
                new SqlParameter("@title_name", SqlDbType.VarChar, 255) { Value = titleName },
                new SqlParameter("@title_price", SqlDbType.Int) { Value = titlePrice },
                new SqlParameter("@title_content", SqlDbType.VarChar, 255) { Value = titleContent }
            };
            ExecuteNonQuery("updateTitle", parameters);
        }
    }
}