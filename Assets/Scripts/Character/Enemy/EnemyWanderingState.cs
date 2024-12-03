using ActionGame;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderingState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 1.0f;
    private const float AnimatorDampTime = 0.5f;
    private const float wanderSpeed = 2f;

    private Vector3 wanderTarget;
    private bool isRotating = false;
    private bool hasCompletedRotation = false;
    private float currentRotationTime = 0f;
    private float rotationDuration = 1f; // How long the rotation takes
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float wanderDistance;

    public EnemyWanderingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
        wanderDistance = Random.Range(stateMachine.wanderDistanceMin, stateMachine.wanderDistanceMax);
        stateMachine.Agent.speed = wanderSpeed;

        // Initialize rotation
        StartRandomRotation();
    }

    public override void Tick(float deltaTime)
    {
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

        if (isRotating)
        {
            // Handle rotation
            RotateRandomly(deltaTime);
        }
        else if (!hasCompletedRotation)
        {
            // Rotation just completed, set up movement
            hasCompletedRotation = true;
            wanderTarget = GetForwardWanderPosition();
            stateMachine.Agent.SetDestination(wanderTarget);
        }
        else
        {
            // Handle movement after rotation is complete
            MoveToWanderTarget(deltaTime);

            if (stateMachine.Agent.remainingDistance <= stateMachine.Agent.stoppingDistance)
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
                return;
            }

            stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
        }
    }

    private void StartRandomRotation()
    {
        isRotating = true;
        currentRotationTime = 0f;
        startRotation = stateMachine.transform.rotation;
        float randomRotation = Random.Range(0f, 360f);
        targetRotation = Quaternion.Euler(0f, randomRotation, 0f);
    }

    private void RotateRandomly(float deltaTime)
    {
        currentRotationTime += deltaTime;
        float t = currentRotationTime / rotationDuration;

        // Use SmoothStep for even smoother rotation
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        // Lerp between start and target rotation
        stateMachine.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, smoothT);

        // Check if rotation is complete
        if (currentRotationTime >= rotationDuration)
        {
            isRotating = false;
        }
    }

    private void MoveToWanderTarget(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            Move(stateMachine.Agent.desiredVelocity.normalized * wanderSpeed, deltaTime);
        }

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }

    private Vector3 GetForwardWanderPosition()
    {
        Vector3 forwardDirection = stateMachine.transform.forward * wanderDistance;
        Vector3 targetPosition = stateMachine.transform.position + forwardDirection;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, wanderDistance, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return stateMachine.transform.position;
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }
}