using System;
using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot{

    public class SearchMap{

        internal IPlayer PlayerInfo;

        internal SearchMap(IPlayer playerInfo, MapAnalyzedUnit[,] mapAnalyzeds, Point point){
            this.mapAnalyzeds = mapAnalyzeds;
            this.housePosition = point;
            this.PlayerInfo = playerInfo;
        }

        private MapAnalyzedUnit[,] mapAnalyzeds;

        private Point housePosition;
        
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
                    Console.Write("Position: " + x + ", " + y + " " + map.GetTileAt(x,y) + " ||");
                }
                counterY = 0;
                counterX++;
            }
            
            //printMap();

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

        internal List<Point> findRessources(){

            List<Point> listResources = new List<Point>();

            for(int i = 0; i < 20; i++){
                for(int j = 0; j < 20; j++){
                    
                    if(mapAnalyzeds[i,j].tileContent == TileContent.Resource){
                        listResources.Add(new Point(mapAnalyzeds[i,j].positionX,mapAnalyzeds[i,j].positionY));
                    }

                    if(mapAnalyzeds[i,j].tileContent == TileContent.House && housePosition == null){
                        housePosition = new Point(mapAnalyzeds[i,j].positionX,mapAnalyzeds[i,j].positionY);
                    }
                }
            }
            /*/
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            foreach(Point point in listResources){
                Console.WriteLine(point.X + " " + point.Y);
            }

            //Console.WriteLine("House Position: " + housePosition.X +","+housePosition.Y);
            */
            return listResources;
        }

    }


}