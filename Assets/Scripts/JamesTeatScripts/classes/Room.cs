using UnityEngine;
using System.Collections;

public class Room  {

	public int room_num;

	private int row;
	private int col;

	public int num_floor = 0;
	public int num_wall = 1;
	public int num_door = 2;

	private Dice d = Dice.getInatance ();
	private NumGen ng = NumGen.getInatance (); 

	
	public int[,] grid;
	
/************************************************/
	public Room(int room_num){
		this.room_num = room_num;
		creatRoom ();
	}
/************************************************/

	private void creatRoom(){
		row = ng.getX ();
		col = ng.getY ();
		grid = new int[row, col];
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++){
				switch(room_num){
				case 0:
					room0_fill(i,j);
					break;
				case 1:
					room1_fill(i,j);
					break;
				case 2:
					room2_fill(i,j);
					break;
				case 3:
					room3_fill(i,j);
					break;
				}
			}
		}
	}


	public int getRow(){return row;}
	public int getCol(){return col;}


	
	private void room0_fill(int i, int j){
		if((i ==(int)row/2)&&(j==0||j==col-1)){
			d.roll();
			if(d.getVal() < d.getMaxVal()){
				grid[i,j] =num_door;
			}
			else{grid[i,j] =num_wall;}
		}
		else if((j ==(int)col/2)&&(i==row-1 || i ==0)){
			d.roll();
			if(d.getVal() < d.getMaxVal()/4){
				grid[i,j] =num_door;
			}
			else{grid[i,j] =num_wall;}
		}
		else if( (i==0 || i == row-1) ){
			grid[i,j]=num_wall;
		}
		else if( (j==0 || j == col-1) ){
			grid[i,j]=num_wall;
		}
		else{
			grid[i,j]=num_floor;
		}
	}
	
	private void room1_fill(int i, int j){
		if((i == (int)row/2)&&(j==0||j==col-1)){
			grid[i,j] =num_door;
		}
		// the bonds of the room COL,
		else if( (i==0 || i == row-1) ){
			grid[i,j]=num_wall;
		}
		// the bonds of the room COL,
		else if( (j==0 || j == col-1) ){
			grid[i,j]=num_wall;
		}
		else{
			grid[i,j]=num_floor;
		}
	}
	
	private void room2_fill(int i, int j){
		if((i ==(int)row/2)&&(j==0||j==col-1)){
			grid[i,j] =num_door;
		}
		else if((j ==(int)col/2)&&(i==row-1 || i ==0)){
			grid[i,j] =num_door;
		}
		else if( (i==0 || i == row-1) ){
			grid[i,j]=num_wall;
		}
		else if( (j==0 || j == col-1) ){
			grid[i,j]=num_wall;
		}
		else{
			grid[i,j]=num_floor;
		}
	}
	
	private void room3_fill(int i, int j){
		if((i ==(int)row/2)&&(j==0||j==col-1)){
			grid[i,j] =num_door;
		}
		else if((j ==(int)col/2)&&(i==0)){
			grid[i,j] =num_door;
		}
		else if( (i==0 || i == row-1) ){
			grid[i,j]=num_wall;
		}
		else if( (j==0 || j == col-1) ){
			grid[i,j]=num_wall;
		}
		else{
			grid[i,j]=num_floor;
		}
	}

}
