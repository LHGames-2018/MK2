using System;
using System.Collections.Generic;
using LHGames.Helper;
using System.Linq;

namespace LHGames.Bot{

    public class SearchMap{

        internal IPlayer PlayerInfo;
        public Point housePosition;
        internal SearchMap(IPlayer playerInfo, MapAnalyzedUnit[,] mapAnalyzeds, Point point){
            this.mapAnalyzeds = mapAnalyzeds;
            housePosition = point;
            this.PlayerInfo = playerInfo;
        }

        private MapAnalyzedUnit[,] mapAnalyzeds;

    
        
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
                    //Console.Write("Position: " + mapAnalyzeds[i,j].positionX + "," + mapAnalyzeds[i,j].positionY + " " + mapAnalyzeds[i,j].tileContent + "||");
                }
                //Console.WriteLine();
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
            /*/
            //Console.WriteLine("House Position: " + housePosition.X +","+housePosition.Y);
            listResources = sortResource(listResources);

            return listResources;
        }

        internal List<Point> sortResource(List<Point> listResources){
            List<Point> newListResource = new List<Point>();

            double distanceMin = 9999;
            foreach(Point point in listResources){

                Point playerInfoPoint = new Point(PlayerInfo.Position.X, PlayerInfo.Position.Y);

                double distance = Point.Distance(playerInfoPoint, point);

                Console.WriteLine("Distance: " + distance + "||" + "For: " + point.X +","+point.Y);

                Console.WriteLine("Count "+newListResource.Count);
                if(newListResource.Count == 0){
                    newListResource.Add(point);
                }
                Console.WriteLine("Count22 "+newListResource.Count);

                if(distance < distanceMin && newListResource.Count > 0){
                    newListResource.Add(point);
                    Swap(newListResource, 0, newListResource.Count - 1);
                    distanceMin = distance;
                }
            }

            Console.WriteLine("Position: "+newListResource[0].X + "," + newListResource[0].Y);
            
            return newListResource;
        }

        private void Swap( List<Point> list, int index1, int index2){
            Point temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

    }


}