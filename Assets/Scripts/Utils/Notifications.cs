/**
 * The list of internal notifications issued by an agent.
 * These are simple object messages that a `MonoBehaviour` may
 * implement. A possible use could be playing an animation or sound,
 * or configuring a component to reflect agent behaviour.
 */
public static class Notifications{

    public const string OnIngest   = "OnIngest",
                        OnGrab     = "OnGrab",
                        OnExpel    = "OnExpel",
                        OnGive     = "OnGive",
                        OnTell     = "OnTell",
                        OnTransfer = "OnTransfer",
                        OnLookAt   = "OnLookAt",
                        OnGesture  = "OnGesture",
                        OnPush     = "OnPush",
                        OnReach    = "OnReach";

}
