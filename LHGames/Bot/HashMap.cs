using System;
using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot{
	
	
	internal class Hash{
		
		private const int tableSize = 50;
		
		
		internal Hash(){
			for(int i < 0 i < tableSize; i++){
				HashTable[i] = new item;
				HashTable[i].player.Position.X = 0;
				HashTable[i].player.Position.Y = 0;
				HashTable[i].points.X = 0;
				HashTable[i].points.Y = 0;
				
			}
			
		}
		struct item{
			PlayerInfo player;
			Point points;
		}
		
		item[] HashTable = new item[tableSize];
		
		int Hash(PlayerInfo player, Point pointRessource){
			
			
			(int) index = Math.sqrt((player.Position.X - pointRessource.X)^2 + (player.Position.X - pointRessource.X)^2);
			return index;
			
		};
		
		void addItem(PlayerInfo player, Point point){
			
			
			int index = Hash(player, point);
			
			if(HashTable[index].player.Position.X == 0 && HashTable[index].player.Position.Y == 0 && HashTable[index].points.X == 0 && HashTable[index].points.Y == 0){
				
				HashTable[index].player.Position.X = player.Position.X;
				HashTable[index].player.Position.Y = player.Position.Y;
				HashTable[index].point.X = point.X;
				HashTable[index].point.Y = point.Y;
				
			}
			
			else{
				
				int newIndex = 0;
				while(HashTable[index].player.Position.X != 0 || HashTable[index].player.Position.Y != 0 || HashTable[index].points.X != 0 || HashTable[index].points.Y != 0){
					int newIndex = index + 1;
					index++;
				}
				
				HashTable[newIndex].player.Position.X = player.Position.X;
				HashTable[newIndex].player.Position.Y = player.Position.Y;
				HashTable[newIndex].point.X = point.X;
				HashTable[newIndex].point.Y = point.Y;
				
			}
			
		}
		
		
		
		
		
	}
	
	
	
	
	
	
	
}


