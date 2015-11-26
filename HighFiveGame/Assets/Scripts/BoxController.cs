using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoxController : MonoBehaviour {
	public int score;
	public GameObject hand;
    public ParticleSystem particles;
	bool inRange = false;
	bool dissapearing = false;
	Color colour = new Color(1, 1, 1, 0);
	Color handColour = new Color (1, 1, 1, 0);
	Vector3 direction = new Vector3 (0, 0, -7);
	bool dead = false;

	Ray ray;
	RaycastHit hit;

    public Material yellowMat;
    public Material redMat;
    private Renderer renderer;
    public LayerMask layerMask;
	// Use this for initialization
	void Awake () {
        renderer = hand.GetComponent<Renderer>();
		this.GetComponent<Renderer>().material.color = colour;

		Vector3 handPos = this.transform.position;
		if (this.transform.position.x < 0) {
			handPos.x += Random.Range (0.5f, this.transform.position.x * -0.5f);
		} else {
            handPos.x -= Random.Range(0.5f, this.transform.position.x * 0.5f);
			hand.transform.localScale = new Vector3(hand.transform.localScale.x *-1, hand.transform.localScale.y, hand.transform.localScale.z);
            particles.transform.localScale = new Vector3(particles.transform.localScale.x * -1, particles.transform.localScale.y, particles.transform.localScale.z);
		}
		handPos.y += Random.Range (0.5f, 2.5f);
		handPos.z -= 0.01f;
		hand.transform.position = handPos;
        particles.Pause();
        AICheck();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += direction * Time.deltaTime;
		CheckIfInRange ();
		CheckCollision ();
		AdjustOpacity ();
		this.GetComponent<Renderer>().material.color = colour;
		hand.GetComponent<Renderer>().material.color = handColour;

	}

	void CheckIfInRange() {
		if (this.transform.position.z < 20 && this.transform.position.z > 0) {
			inRange = true;
		} else {
			inRange = false;
		}

		if (!dissapearing) {
			if (inRange) {
                renderer.material = redMat;
			} else {
                renderer.material = yellowMat;
			}
		}
	}

	void AdjustOpacity() {
		if (!dissapearing) {
			if (colour.a < 1.0f) {
				colour.a += Time.deltaTime;
				handColour.a += Time.deltaTime;
			}
		}
		else {
			handColour.a = handColour.a - Time.deltaTime;
			if(handColour.a <= 0f) {
				Destroy();
			}
		}
	}

	Vector2 GetTouchPos() {
		Vector2 inputPos = new Vector2 (-1, -1);
		#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
		if (Input.GetMouseButtonDown (0)) {
			inputPos = Input.mousePosition;
		}
		#else
		if(Input.touchCount > 0) {
			Touch myTouch = Input.touches[0];
			if(myTouch.phase == TouchPhase.Began) {
				inputPos = myTouch.position;
			}
		}
		#endif
		return inputPos;
	}

	void CheckCollision() {
		Vector2 inputPos = GetTouchPos ();
        if (inputPos.x != -1) {
		    ray = Camera.main.ScreenPointToRay (inputPos);
            if (Physics.Raycast(ray, out hit, layerMask)) {
                if (hit.collider.gameObject == hand)
                {

                    Collide();
                }
                   
            }
		}
	}

	void Collide() {
		if (inRange && !dissapearing) {
			dissapearing = true;
			++GameManager.instance.playerConsecutive;
            particles.transform.position = hand.transform.position;
            particles.Emit(50);
            SoundManager.Instance.Slap();
            renderer.enabled = false;
			Dissapear();
		}
	}

	void Destroy() {
        if (dissapearing == false)
        {
            GameManager.instance.playerReset();
        }
	}

	void OnBecameInvisible() {
        if (!dissapearing) {
            Destroy();
        }
		GameObject.Destroy(gameObject);

	}

	void Dissapear()
	{
		if (!dead) {
			if (dissapearing) {
				GameManager.playerScore += 1 * GameManager.instance.playerMultiplier;
			} else {
				GameManager.instance.playerMultiplier = 1;
			}
		}
		dead = true;
	}

    void AICheck()
    {
        if ((Random.Range(0, 100)) < 90)
        {
            GameManager.enemyScore += 1 * GameManager.instance.enemyMultiplier;
            ++GameManager.instance.enemyConsecutive;
            GameManager.instance.enemyMulti();
        }
        else
        {
            GameManager.instance.enemyReset();
        }
    }
}
