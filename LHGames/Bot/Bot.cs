﻿using System;
using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot
{
    internal class Bot
    {
        internal IPlayer PlayerInfo { get; set; }
        private int _currentDirection = 1;
<<<<<<< HEAD

        private MapAnalyzedUnit[,] mapAnalyzeds = new MapAnalyzedUnit[20,20];

        //mapAnalyzeds[0]
=======
		int pass = 3;
>>>>>>> 05ed020bcf06114471a93722d96119cbf867828a
        internal Bot() { }

        /// <summary>
        /// Gets called before ExecuteTurn. This is where you get your bot's state.
        /// </summary>
        /// <param name="playerInfo">Your bot's current state.</param>
        internal void BeforeTurn(IPlayer playerInfo)
        {
            PlayerInfo = playerInfo;
        }

        /// <summary>
        /// Implement your bot here.
        /// </summary>
        /// <param name="map">The gamemap.</param>
        /// <param name="visiblePlayers">Players that are visible to your bot.</param>
        /// <returns>The action you wish to execute.</returns>
		
		internal int gotoHouseX(){
			
			if(pass > 0){
				pass = pass - 1;
				return 1;
				
			}
			
			else{
				return 0;
			}
		}
		
        internal string ExecuteTurn(Map map, IEnumerable<IPlayer> visiblePlayers)
        {
            // TODO: Implement your AI here.
			
			
            if (map.GetTileAt(PlayerInfo.Position.X + _currentDirection, PlayerInfo.Position.Y) == TileContent.Wall)
            {
                _currentDirection *= -1;
            }

            analyseMap(map);

            var data = StorageHelper.Read<TestClass>("Test");
            Console.WriteLine(data?.Test);
            return AIHelper.CreateMoveAction(new Point(0, 1));
        }

        /// <summary>
        /// Gets called after ExecuteTurn.
        /// </summary>
        internal void AfterTurn()
        {
        }

        internal void analyseMap(Map map){

            //Start Map
            instanciateMap();

            int currentX = PlayerInfo.Position.X;
            int currentY = PlayerInfo.Position.Y;

            int beginArrayX = currentX - 10;
            int beginArrayY = currentY - 10;

            int endArrayX = currentX + 10;
            int endArrayY = currentY + 10;

            int counterX = 0;
            int counterY = 0;

            for(int x = beginArrayX; x < endArrayX; x++){
                for(int y = beginArrayY; y < endArrayY; y++){

                    mapAnalyzeds[counterX,counterY].positionY = y; 
                    mapAnalyzeds[counterX,counterY].positionX = x;
                    mapAnalyzeds[counterX,counterY].tileContent = map.GetTileAt(x,y);

                    counterY++;
                    //Console.Write("Position: " + x + ", " + y + "||" + map.GetTileAt(x,y) + " ");
                }
                counterY = 0;
                counterX++;
            }
            
            printMap();

        }

        internal void instanciateMap(){
            for(int i = 0; i < 20; i++){
                for(int j = 0; j < 20; j++){
                    mapAnalyzeds[i,j] = new MapAnalyzedUnit(); 
                }
            }
        }

        internal void printMap(){
            for(int i = 0; i < 20; i++){
                for(int j = 0; j < 20; j++){
                    Console.Write("Position: " + mapAnalyzeds[i,j].positionX + "," + mapAnalyzeds[i,j].positionY + " " + mapAnalyzeds[i,j].tileContent + "||");
                }
                Console.WriteLine();
            }
        }



    }
}

class TestClass
{
    public string Test { get; set; }
}