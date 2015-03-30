using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding2D : MonoBehaviour
{
    public List<Vector3> Path = new List<Vector3>();
    public bool JS = false;

    public void FindPath(Vector3 startPosition, Vector3 endPosition)
    {
        Pathfinder2D.Instance.InsertInQueue(startPosition, endPosition, SetList);
    }

    public void FindJSPath(Vector3[] arr)
    {
        if (arr.Length > 1)
        {
            Pathfinder2D.Instance.InsertInQueue(arr[0], arr[1], SetList);
        }
    }

    //A test move function, can easily be replaced
    public void Move()
    {
		Vector2 moveDirection;
        if (Path.Count > 0)
        {
			moveDirection = new Vector2(transform.position.x - Path[0].x , transform.position.y - Path[0].y );



			moveDirection.Normalize();
			float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;


			transform.rotation = 
				Quaternion.Slerp( transform.rotation, 
				                 Quaternion.Euler( 0, 0, targetAngle ), 
				                 5f * Time.deltaTime );
            transform.position = Vector3.MoveTowards(transform.position, Path[0], Time.deltaTime*2F);

			//transform.position = Vector2.Lerp (transform.position, Path[0], Time.deltaTime);
            if (Vector3.Distance(transform.position, Path[0]) < 0.4F)
            {
                Path.RemoveAt(0);
            }
        }
    }

    protected virtual void SetList(List<Vector3> path)
    {
        if (path == null)
        {
            return;
        }

        if (!JS)
        {
            Path.Clear();
            Path = path;
            Path[0] = new Vector3(Path[0].x, Path[0].y, Path[0].z);
            Path[Path.Count - 1] = new Vector3(Path[Path.Count - 1].x, Path[Path.Count - 1].y, Path[Path.Count - 1].z);
        }
        else
        {           
            Vector3[] arr = new Vector3[path.Count];
            for (int i = 0; i < path.Count; i++)
            {
                arr[i] = path[i];
            }

            arr[0] = new Vector3(arr[0].x, arr[0].y , arr[0].z);
            arr[arr.Length - 1] = new Vector3(arr[arr.Length - 1].x, arr[arr.Length - 1].y, arr[arr.Length - 1].z);
            gameObject.SendMessage("GetJSPath", arr);
        }
    }
}
