using System;
using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot
{
    internal class Bot
    {
        internal IPlayer PlayerInfo { get; set; }
        private int _currentDirection = 1;

        private MapAnalyzedUnit[,] mapAnalyzeds = new MapAnalyzedUnit[20, 20];

        private Point housePosition;

        private SearchMap searchMap;

        internal Bot() { }

        /// <summary>
        /// Gets called before ExecuteTurn. This is where you get your bot's state.
        /// </summary>
        /// <param name="playerInfo">Your bot's current state.</param>
        internal void BeforeTurn(IPlayer playerInfo)
        {
            PlayerInfo = playerInfo;
            searchMap = new SearchMap(playerInfo, mapAnalyzeds, housePosition);
        }

        /// <summary>
        /// Implement your bot here.
        /// </summary>
        /// <param name="map">The gamemap.</param>
        /// <param name="visiblePlayers">Players that are visible to your bot.</param>
        /// <returns>The action you wish to execute.</returns>

        internal string ExecuteTurn(Map map, IEnumerable<IPlayer> visiblePlayers)
        {
            // TODO: Implement your AI here.


            // if (map.GetTileAt(PlayerInfo.Position.X + _currentDirection, PlayerInfo.Position.Y) == TileContent.Wall)
            // {
            //     _currentDirection *= -1;
            // }

            searchMap.analyseMap(map);
            searchMap.findRessources();

            var data = StorageHelper.Read<TestClass>("Test");
            // Console.WriteLine(data?.Test);
            return returnMoveAction(searchMap.findRessources(), PlayerInfo);
        }
        public Point calculateTileDistance(List<Point> ressourcePositions, IPlayer playerInfor)
        {
            var xDistance = ressourcePositions[0].X - playerInfor.Position.X;
            var yDistance = ressourcePositions[0].Y - playerInfor.Position.Y;
            return new Point(xDistance, yDistance);
        }

        public Point calculateHouseDistance(Point housePosition, IPlayer playerInfor)
        {
            var xDistance = housePosition.X - playerInfor.Position.X;
            var yDistance = housePosition.Y - playerInfor.Position.Y;
            return new Point(xDistance, yDistance);
        }

        public int randomize()
        {
            Random rand = new Random();
            int number = rand.Next(0, 2);
            if (number == 1)
            {
                return 1;
            }
            else
                return 0;
        }
        public string returnMoveAction(List<Point> ressourcePositions, IPlayer playerInfor)
        {
            var distance = calculateTileDistance(ressourcePositions, playerInfor);
            var houseDistance = calculateHouseDistance(searchMap.housePosition, playerInfor);
            Console.WriteLine("Capacity: "+playerInfor.CarriedResources);
            if (playerInfor.CarriedResources == 500)//playerInfor.CarryingCapacity)
            {
                return moveToHouse(houseDistance, playerInfor);
            }
            return moveToRessource(distance, ressourcePositions);

        }

        private string moveToRessource(Point distance, List<Point> ressourcePositions)
        {

            
            if (distance.X != 0)
            {
                return distance.X > 0 ? AIHelper.CreateMoveAction(new Point(1, 0)) : AIHelper.CreateMoveAction(new Point(-1, 0));
            }
            if ((int)Point.DistanceSquared(ressourcePositions[0], PlayerInfo.Position) == 1)
            {
                return AIHelper.CreateCollectAction(miningPosition(ressourcePositions[0], PlayerInfo.Position));
            }
            else if (distance.Y != 0)
            {
                return distance.Y > 0 ? AIHelper.CreateMoveAction(new Point(0, 1)) : AIHelper.CreateMoveAction(new Point(0, -1));
            }
            return "";
        }

        private string moveToHouse(Point houseDistance, IPlayer playerInfor)
        {

            if (houseDistance.X != 0)
            {
                // if (ressourcePositions[0].X - PlayerInfo.Position.X == 1){

                //     Random rand = new Random();
                //     int number = rand.Next(0,1);
                //     return AIHelper.CreateMoveAction(new Point(0,randomize()));
                // }
                return houseDistance.X > 0 ? AIHelper.CreateMoveAction(new Point(1, 0)) : AIHelper.CreateMoveAction(new Point(-1, 0));
            }
            else if (houseDistance.Y != 0)
            {
                // if (ressourcePositions[0].Y - PlayerInfo.Position.Y == 1){
                //     Random rand = new Random();
                //     int number = rand.Next(0,1);
                //     return AIHelper.CreateMoveAction(new Point(randomize(),0));

                // }                    
                return houseDistance.Y > 0 ? AIHelper.CreateMoveAction(new Point(0, 1)) : AIHelper.CreateMoveAction(new Point(0, -1));
            }
            return AIHelper.CreateMoveAction(new Point(0, 1));

        }

        private Point miningPosition(Point resourcePosition, Point playerPosition)
        {
            int x = resourcePosition.X - playerPosition.X;
            int y = resourcePosition.Y - playerPosition.Y;

            return new Point(x, y);

        }



        // public Point ressourceDirection(IPlayer playerInfor)
        // {
        //     Map map;
        //     map.GetTileAt()
        //     return new Point();
        // }
        /// <summary>
        /// Gets called after ExecuteTurn.
        /// </summary>
        internal void AfterTurn()
        {
        }

    }
}

class TestClass
{
    public string Test { get; set; }
}