using System;
using System.Collections.Generic;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        /// <summary>
        ///  This is the address of the database.
        ///  We define it here as a constant since it will never change.
        /// </summary>
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            Console.WriteLine("Getting All Rooms:");
            Console.WriteLine();

            List<Room> allRooms = roomRepo.GetAll();

            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }

            Room bathroom = new Room
            {
                Name = "Bathroom",
                MaxOccupancy = 1
            };

            roomRepo.Insert(bathroom);

            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Added the new Room with id {bathroom.Id}");

            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Room with Id 1");

            Room singleRoom = roomRepo.GetById(1);

            Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");

            Console.WriteLine("Delete room by Id");
            roomRepo.Delete(9);
            Console.WriteLine("deleted successfully");
            Console.WriteLine("update room");
            bathroom.Name = "dinning room";
            bathroom.MaxOccupancy = 5;
            roomRepo.Update(bathroom);
            Console.WriteLine("updated successfully");


            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            List<Roommate> roommates = roommateRepo.GetAll();

            foreach(Roommate roommate in roommates)
            {
                Console.WriteLine(@$"First Name: {roommate.Firstname}
                Last Name: {roommate.Lastname}
                Rent Portion: {roommate.RentPortion}
                Move In Date: {roommate.MovedInDate}");
            }
            Room room1 = roommateRepo.GetById(1).Room;
            Roommate roommate1 = roommateRepo.GetById(1);
            Console.WriteLine(@$"Roommate First Name: {roommate1.Firstname} 
                              Last Name: {roommate1.Lastname}
                              Move In Date { roommate1.MovedInDate.AddDays(-1)}
                              Rent Portion {roommate1.RentPortion}
                              Room Name: {room1.Name}
                              Maximum occupancy of room {room1.MaxOccupancy}
                              ");

        }
    }
}