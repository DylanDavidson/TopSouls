using UnityEngine;
using System.Collections;

public class Obstical {
	public int obstical_num;
	
	private int row;
	private int col;
	
	public int num_floor = 1;
	public int num_wall = 2;
	public int num_door = 3;
	
	private Dice d = Dice.getInatance ();
	private NumGen ng = NumGen.getInatance (); 
	
	
	public int[,] grid;

	public Obstical (int obstical_num, int row, int col){
		this.obstical_num = obstical_num;
		this.row = row;
		this.col = col;
		creatObstical ();
	}

	public int getRow (){return row;}
	public int getCol (){return col;}

	private bool Check(int i, int j){
		if (row > 5 && col > 5 && i>1 && i<row-2 
		    && j>1 && j<col-2) {
			return true;
		}
		else {
			return false;
		}
	}

	public void creatObstical(){
		grid = new int[row, col];
		for (int i=0; i<row; i++) {
			for(int j=0; j<col;j++){
				switch(obstical_num){
				case 0:
				if(row > 5 && col > 5 && i>1 && i<row-2 
					   && j>1 && j<col-2){
						
						d.roll();
						if(d.getVal() < d.getMaxVal()-1){
							//grid[i,j] = num_floor;
						}
						else{
							grid[i,j] = num_wall;
						}
					}
				else{
					//grid[i,j]=num_floor;
				}
					break;
				}
			}
		}
	}

	public void obstical_0_fill(int i, int j){
			d.roll();
			if (d.getVal () > d.getMaxVal ()-2) {
				Debug.Log(d.getVal ());
				grid [i, j] = num_wall;
			}
	}


}
