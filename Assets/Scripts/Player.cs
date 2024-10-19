using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour {

	public float jumpvelocity;
	public float gravity;
	public Vector2 velocity;
	private bool walk = false, walk_left = false, walk_right = false, jump = false;
	public LayerMask wallMask = 0; // muss im Player-Inspektor auf Default gesetzt werden.
	public LayerMask floorMask; 

	public enum PlayerState {
		jumping,
		idle,
		walking
	}

	private PlayerState playerState = PlayerState.idle;
	private bool grounded = false;

	// Use this for initialization
	void Start () {
		Fall ();  
	}

	void Update () {
		CheckPlayerInput ();
		UpdatePlayerPosition ();
	}

    void CheckPlayerInput()
    {

        walk_left = Input.GetKey(KeyCode.A);

        walk_right = Input.GetKey(KeyCode.D);

        walk = walk_right || walk_left;

        jump = Input.GetKeyDown(KeyCode.W);
    }
    void UpdatePlayerPosition() {

		Vector3 pos = transform.localPosition;
		Vector3 scale = transform.localScale;

		if (walk) {
			if (walk_left) {
				pos.x -= velocity.x * Time.deltaTime;
				scale.x = -1;
            }

			if (walk_right) {
				pos.x += velocity.x * Time.deltaTime;
				scale.x = 1;
            }
            //pos = CheckWallRays (pos, scale.x);
        }

		if (jump && (playerState != PlayerState.jumping)) {
			playerState = PlayerState.jumping;
			velocity = new Vector2 (velocity.x, jumpvelocity);
		}

		if (playerState == PlayerState.jumping) {
			pos.y += velocity.y * Time.deltaTime;
			velocity.y -= gravity * Time.deltaTime;
		}

		if (velocity.y <= 0)  
			pos = CheckFloorRays (pos);

		transform.localPosition = pos;
		transform.localScale = scale;
		walk = false;
		walk_left = false;
		walk_right = false;
		jump = false;

    }


	void Fall() {

		// Player nach unten "ziehen". Springen ist = Fallen.
		velocity.y = 0;
		playerState = PlayerState.jumping;
		grounded = false;

	}

	Vector3 CheckWallRays(Vector3 pos, float direction) {


		Vector2 originTop = new Vector2(pos.x + direction * 0.4f, pos.y + 1f - 0.2f);
		Vector2 originMiddle = new Vector2 (pos.x + direction * 0.4f, pos.y);
		Vector2 originBottom = new Vector2(pos.x + direction * 0.4f, pos.y - 1f + 0.2f);
		RaycastHit2D wallTop = Physics2D.Raycast (originTop, new Vector2 (direction, 0), velocity.x * Time.deltaTime, wallMask);
		RaycastHit2D wallMiddle = Physics2D.Raycast (originMiddle, new Vector2 (direction, 0), velocity.x * Time.deltaTime, wallMask);
		RaycastHit2D wallBottom = Physics2D.Raycast (originBottom, new Vector2 (direction, 0), velocity.x * Time.deltaTime, wallMask);

		if (wallTop.collider != null) {
			pos.x -= velocity.x * Time.deltaTime * direction;
		}

		return pos;
	}

	Vector3 CheckFloorRays(Vector3 pos) {
		Vector2 originLeft = new Vector2 (pos.x - 0.5f + 0.2f, pos.y - 1f);
		Vector2 originMiddle = new Vector2 (pos.x, pos.y - 1f);
		Vector2 originRight = new Vector2 (pos.x + 0.5f - 0.2f, pos.y - 1f);

		RaycastHit2D floorLeft = Physics2D.Raycast (originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
		RaycastHit2D floorMiddle = Physics2D.Raycast (originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
		RaycastHit2D floorRight = Physics2D.Raycast (originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);

		if (floorLeft.collider != null || floorMiddle.collider != null || floorRight.collider != null) {

			RaycastHit2D hitRay = floorRight;

			if (floorLeft) {
				hitRay = floorLeft;
			} else if (floorMiddle) {
				hitRay = floorMiddle;
			} else if (floorMiddle) {
				hitRay = floorMiddle;
			} 

			playerState = PlayerState.idle;
			grounded = true;
			velocity.y = 0;

			pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 1;

		} else {
			
			if (playerState != PlayerState.jumping) {
				Fall ();
			}
		}

		return pos;
	}
}
