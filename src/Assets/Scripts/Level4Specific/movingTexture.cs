using UnityEngine;
using System.Collections;

public class movingTexture : MonoBehaviour {
    public int materialIndex = 0;
    public Vector2 speed = new Vector2( 1.0f, 0.0f );
    public string textureName = "_MainTex";
	private Vector2 temp;
	public Vector2 tiling= new Vector2(1.0f,1.0f);
 
    Vector2 uvOffset = Vector2.zero;
 
	void Awake(){
	}
	
    void FixedUpdate() 
    {
		temp.x=(speed.x*tiling.x)/GetComponent<Renderer>().bounds.size.x;
		temp.y=(speed.y*tiling.y)/GetComponent<Renderer>().bounds.size.z;
		
        uvOffset += ( temp * Time.deltaTime );
        if( GetComponent<Renderer>().enabled )
        {
            GetComponent<Renderer>().materials[ materialIndex ].SetTextureOffset( textureName, uvOffset );
        }
    }
}

