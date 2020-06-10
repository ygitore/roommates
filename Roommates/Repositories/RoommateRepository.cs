using Microsoft.Data.SqlClient;
using Roommates.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roommates.Repositories
{
    class RoommateRepository: BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString)
        {

        }
        public List<Roommate> GetAll()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName, RentPortion, MoveInDate, RoomId FROM Roommate";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Roommate> roommate = new List<Roommate>();
                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");

                        // We user the reader's GetXXX methods to get the value for a particular ordinal.
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int rentPortionColumnPosition = reader.GetOrdinal("RentPortion");
                        int rentPortionValue = reader.GetInt32(rentPortionColumnPosition);

                        int moveInDateColumnPosition = reader.GetOrdinal("MoveInDate");
                        DateTime moveInDateValue = reader.GetDateTime(moveInDateColumnPosition);
                        

                        Roommate newRoommate = new Roommate()
                        {
                            Id = idValue,
                            Firstname = firstNameValue,
                            Lastname = lastNameValue,
                            RentPortion = rentPortionValue,
                            MovedInDate = moveInDateValue,
                            Room = null
                        };

                        roommate.Add(newRoommate);
                    }
                    conn.Close();
                    return roommate;
                }
            }
        }
        public Roommate GetById(int id)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT roommate.Id, 
                                        roommate.FirstName, 
                                        roommate.LastName,
                                        roommate.RentPortion, 
                                        roommate.MoveInDate, 
                                        room.Id, 
                                        room.Name, 
                                        room.MaxOccupancy 
                                      FROM Roommate roommate 
                                      JOIN Room room ON room.Id = roommate.RoomId 
                                      WHERE roommate.RoomId = @roomId";
                    cmd.Parameters.AddWithValue("@roomId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Roommate newRoommate = null;
                    while (reader.Read())
                    {
                        int id_value = reader.GetInt32(reader.GetOrdinal("roommate.Id"));
                        string firstName_value = reader.GetString(reader.GetOrdinal("roommate.FirstName"));
                        string lastName_value = reader.GetString(reader.GetOrdinal("roommate.LastName"));
                        int rentPortion_value = reader.GetInt32(reader.GetOrdinal("roommate.RentPortion"));
                        DateTime moveInDate_value = reader.GetDateTime(reader.GetOrdinal("roommate.MoveInDate"));
                        int roomId_value = reader.GetInt32(reader.GetOrdinal("room.RoomId"));

                        int roomateId_value = reader.GetInt32(reader.GetOrdinal("room.Id"));
                        string roomName_value = reader.GetString(reader.GetOrdinal("room.Name"));
                        int roomMaxOccupancy_value = reader.GetInt32(reader.GetOrdinal("room.MaxOccupancy"));
                        newRoommate = new Roommate()
                        {
                            Id = id_value,
                            Firstname = firstName_value,
                            Lastname = lastName_value,
                            RentPortion = rentPortion_value,
                            MovedInDate = moveInDate_value,
                            Room = new Room()
                            {
                                Id = roomId_value,
                                Name = roomName_value,
                                MaxOccupancy = roomMaxOccupancy_value
                            }
                        };
                    }
                    reader.Close();
                    return newRoommate;
                }
            }
        }
    }
}
