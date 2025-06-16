using UnityEngine;

public class ClimbUpPoint : ClimbPointBase
{
    public override void Execute(CharacterClimbController controller)
    {
        controller.StartClimb(mountPoint.position, Quaternion.LookRotation(-transform.forward));
    }
}
