using UnityEngine;
using System.Collections;

public class Obstical {
	public int obstical_num;

	private int numEnemy_inRoom = 10;
	private int row;
	private int col;
	
	private Dice d = Dice.getInatance (); 

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
					if(row > 5 && col > 5 && i>1 && i<row-2 && j>1 && j<col-2){
							obstical_0_fill(i,j);
					}
					break;
				
				case 5:
					obstical_final_fill(i,j);
					break;
				
				}
			}
		}
	}

	public void obstical_final_fill(int i, int j){
		d.roll();
		if (i == row/2 && j == col/2) {
			grid[i,j] = GameVars.num_exit;
		}
		else if (d.getVal () > d.getMaxVal ()-2 && !placement(i,j,4)) {
			if(row > 5 && col > 5 && i>1 && i<row-2 && j>1 && j<col-2){
				grid [i, j] = GameVars.num_wall;
			}
		}
	}

	public void obstical_0_fill(int i, int j){
		d.roll();
		if ((d.getVal () == 3 || d.getVal () == 2) && numEnemy_inRoom != 0 && placement(i,j,4)) {
			d.roll();
			if(d.getVal() ==3){
				grid[i,j] = GameVars.num_enemySpawn;
				numEnemy_inRoom--;
			}
		}
		if (d.getVal () > d.getMaxVal ()-2 ) {
			grid [i, j] = GameVars.num_wall;
		}
	}

	public bool placement (int i, int j, int divider){
		if (i < 0 && j < 0 && i> row && j>col) {
			return false;
		}
		if (i > row / divider && j > col / divider && i < row - row / divider && j < col - col / 4) {
			return true;
		}
		else {
			return false;
		}
	}


}
