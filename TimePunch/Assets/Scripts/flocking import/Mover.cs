using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour {
	Vector3 acceleration;
	Vector3 position;
    public Vector3 Futurepos { get { return (position + 3*velocity); } }
    public Vector3 velocity;
    public Vector3 lastPosition;
	public float mass = 1f;
	public float maxSpeed = 0.5f;
	public float maxTurn = 0.25f;
	public float radius = 0.5f;

    protected bool Collides(Mover v)
    {
        if ((v.transform.position - transform.position).magnitude < (radius + v.radius))
        {
            return true;
        }
            return false;
    }
	protected virtual void Awake () {
        // get our position from the game object
        position = transform.position;
        lastPosition = position;
		// no forces to start off
		velocity = Vector3.zero;
		acceleration = Vector3.zero;
	}
    protected Vector3 Flock(Vector3 dVelocity,List<Mover> Homies,float dist,float sepW,float speedW, float convW)
    {
        Vector3 aPos = new Vector3();
        Vector3 desiredVelocity = dVelocity;
        Vector3 steeringForce = (desiredVelocity - velocity)*speedW;//approach avg velocity
        foreach (var item in Homies)
        {
            aPos += item.position;
            if ((Mover)item != this)
            {
                if ((transform.position - item.transform.position).sqrMagnitude < dist * dist)//run if too close
                {
                    steeringForce += Flee(item.transform.position) * sepW;
                }
                
            }
            
        }
        aPos /= Homies.Count;
        steeringForce += Seek(aPos)*convW;//seek center
        
        
        return VectorHelper.Clamp(steeringForce, maxTurn);
    }
    /// <summary>
    /// avoid the object at the specified position with a gap
    /// </summary>
    /// <param name="Obposition"></param>
    /// <param name="objrad"></param>
    /// <param name="gap">must be positive or zero!</param>
    /// <param name="safedist"></param>
    /// <returns></returns>
    protected Vector3 Avoid(Vector3 Obposition, float objrad,float gap, float safedist)
    {
        Vector3 path = transform.position - Obposition;
        Vector3 point = transform.position + transform.forward;
        float distoffset = Vector3.Dot(transform.right, path);
        if (Mathf.Abs(distoffset) < objrad + radius&&Vector3.Dot(transform.forward,path)>0f&& Vector3.Dot(transform.forward, path) < safedist)
        {
            if (distoffset >= 0f)
            {//on the left (obj on the right)
                return Seek(Obposition - transform.right * (objrad + radius));
            }
            else
            {
                return Seek(Obposition - transform.right * (objrad + radius));
            }
        }
        return Vector3.zero;
    }
	// Will be overridden by derived classes.
	// No direct implementation here!
	protected abstract void CalcSteering ();

    protected Vector3 Evade(GameObject obstacle,float safedist)
    {
        float obstacleRadius = 0;
        float agentRadius = 0;
        if (obstacle.GetComponent<CapsuleCollider>() != null)
        {
             obstacleRadius = obstacle.GetComponent<CapsuleCollider>().bounds.size.x;
             agentRadius = GetComponent<CapsuleCollider>().bounds.size.x;
        }
        else
        {
            return Vector3.zero;
        }
        Vector3 PathToObj = obstacle.transform.position - transform.position;
        Vector3 Closestpoint = velocity.normalized * Vector3.Dot(velocity.normalized, PathToObj);
        Vector3 PathToPoint = Closestpoint-obstacle.transform.position;
        if (PathToPoint.sqrMagnitude > (PathToPoint.normalized * (safedist+obstacleRadius+agentRadius)).sqrMagnitude)
        {
            return Seek(position + PathToObj + PathToPoint.normalized * (safedist + obstacleRadius + agentRadius));
        }
        return Seek(Closestpoint);

    }
    protected Vector3 Aoid(GameObject obstacle, float safeDist)
    {
        float obstacleRadius = obstacle.GetComponent<CapsuleCollider>().bounds.size.x+ .1f;
        float agentRadius = GetComponent<CapsuleCollider>().bounds.size.x+.1f;
        Vector3 obstacleCenter = obstacle.transform.position;
        //Putting it on a 2d plane
        obstacleCenter.y = 0;
        float sumOfRadii = obstacleRadius + agentRadius;
        //Getting the dot products to see whether its too far right or left or if its behind
        double dotProductRight = Vector3.Dot((obstacle.transform.position - position), transform.right);
        double dotProductForward = Vector3.Dot((obstacle.transform.position - position), transform.forward);
        //Eliminating all obstacles not forward
        if (dotProductForward > 0)
        {
            //Checking to see if obstacle is within the 'vision' of agents
            if ((obstacle.transform.position - position).magnitude < safeDist)
            {

                Vector3 desiredVelocity;
                Vector3 steeringForce;
                //Checking to see if its too far right or left
                if (dotProductRight < sumOfRadii || -dotProductRight < -sumOfRadii)
                {
                    //If obstacle is more right go left
                    if (dotProductRight > 0)
                    {
                        desiredVelocity = -transform.right * maxSpeed;
                        
                        steeringForce = desiredVelocity.normalized * maxSpeed;
                        return steeringForce;
                    }
                    //If obstacle is more left go right
                    else
                    {
                        desiredVelocity = transform.right * maxSpeed;
                        steeringForce = desiredVelocity.normalized * maxSpeed;
                        return steeringForce;
                    }
                }
                return Vector3.zero;
            }
            else
            {
                return Vector3.zero;
            }
        }
        else
        {
            return Vector3.zero;
        }
    }
    protected Vector3 Wander()
    {
        Vector3 Direction = velocity.normalized;
        Vector3 WanderPos = position + (Direction + (new Vector3(Random.value-.5f, 0, Random.value-.5f)).normalized*.3f).normalized;
        return Seek(WanderPos);
        
    }
    protected Vector3 Pursue(Mover v)
    {
        return Seek(v.Futurepos);
    }

    // returns a steering vector towards the given targetPos
    protected Vector3 Seek(Vector3 targetPos) {
		Vector3 toTarget = targetPos - position;
        
		Vector3 desiredVelocity = toTarget.normalized * maxSpeed;
		Vector3 steeringForce = desiredVelocity - velocity;

		return VectorHelper.Clamp (steeringForce, maxTurn);
	}

	// should return a steering vector away from the given targetPos
	protected Vector3 Flee(Vector3 targetPos) {
        Vector3 direction = position-targetPos;
        direction.y = 0;
        Vector3 desiredVelocity = direction.normalized * maxSpeed;
        Vector3 steeringForce = desiredVelocity - velocity;
        return VectorHelper.Clamp(steeringForce, maxTurn);
            }

	// returns a steering vector that decreases within threshold distance, stopping at radii
	protected Vector3 Arrive(Vector3 targetPos, float threshold, float radii) {
		Vector3 toTarget = targetPos - position;
		Vector3 desiredVelocity;

		if (toTarget.magnitude - radii < threshold) {
			// slow down
			float percentFromCenter = (toTarget.magnitude - radii) / threshold;
			float fractionOfMaxSpeed = percentFromCenter * maxSpeed;

			desiredVelocity = toTarget.normalized * fractionOfMaxSpeed;
		} else {
			// max speed!
			desiredVelocity = toTarget.normalized * maxSpeed;
		}
			
		Vector3 steeringForce = desiredVelocity - velocity;

		return VectorHelper.Clamp (steeringForce, maxTurn);
	}
    protected Vector3 ArriveVel(Vector3 targetPos, float threshold, float radii)
    {
        Vector3 toTarget = targetPos - position;
        Vector3 desiredVelocity;

        if (toTarget.magnitude - radii < threshold)
        {
            // slow down
            float percentFromCenter = (toTarget.magnitude - radii) / threshold;
            float fractionOfMaxSpeed = percentFromCenter * maxSpeed;

            desiredVelocity = toTarget.normalized * fractionOfMaxSpeed;
        }
        else
        {
            // max speed!
            desiredVelocity = toTarget.normalized * maxSpeed;
        }

        Vector3 steeringForce = desiredVelocity - velocity;

        return desiredVelocity;
    }
	public void ApplyForce(Vector3 force) {
		acceleration += force / mass;
	}
		
	void LateUpdate () {

		CalcSteering ();

		// update velocity, position
		velocity += acceleration;
		velocity = VectorHelper.Clamp (velocity, maxSpeed);
        lastPosition = position;
		position += velocity * Time.deltaTime;
        
        //transform.rotation = Quaternion.LookRotation(velocity);
        //transform.rotation = Quaternion.Euler(Mathf.Atan2(velocity.y, velocity.z) * Mathf.Rad2Deg, Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg, Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
        // update the transform so we actually move
        transform.position = position;
		acceleration = Vector3.zero;
	}

	protected virtual void OnRenderObject() {
		//ColorHelper.black.SetPass (0); this threw null stuff and i dont know what it does so i got rid of it

		GL.Begin (GL.LINES);
		GL.Vertex (transform.position);
		GL.Vertex (transform.position + velocity.normalized);
		GL.End ();
	}
}
