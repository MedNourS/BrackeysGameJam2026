// C# Uses a lot of the same concepts as Java
// There are some new parts, but the syntax is mostly the same

// Unity adds a lot of new features to integrate with the engine
// Any of your scripts that want to interact with the engine will have:
// using UnityEngine;
using UnityEngine;

// This is the class definition
// THe : Monobehaviour says "this class inherits from MonoBehaviour
// Classes that inherit from MonoBehaviour have access to special methods such as Start() and Update()
// And all othe lifetime methods unity provides
public class IntroToCSharp : MonoBehaviour
{

    /// FIELDS

    // C# Types are the same as Java, BUT:
    /// Use float instead of double
    /// Use string instead of String

    // C# + Unity has the same public and private fields that you've seen in java
    public static string PublicFieldThatBelongsToClass;
    public string PublicField = "default value"; // Default value

    private int PrivateField;

    protected string ProtectedField = "This field is accessible to all classes that inherit from this class" +
        "Ex through:  public class OtherClass : IntroToCSharp {}";

    // Note that all public non-static fields WILL BE VISIBLE IN EDITOR to reassign
    // However, public fields should ONLY BE USED for values that should be VISIBLE BETWEEN SCRIPTS
    // If you want something that is VISIBLE IN INSPECTOR but PRIVATE, use [SerializeField]
    /// Visible in inspector but hidden from other classes
    [SerializeField] private float speed = 2.5f;

    // Useful for gameobjects/components you want to assign in-editor (Drag-and-drop)
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject crate;


    // C# also has get and set methods
    // These are VERY useful if you want something to be publically accessible, but dont want to risk
    // having it be overwritten in another script

    /// Syntax: <protectionLevel> var foo { <protectionLevel> get; <protectionLevel> set; }
    public int enemyHealth { get; private set; } // Can only be set in this class
                                                 // Note that protection levels must be more secure than the base protection level:
    /// WRONG: private int enemyHealth { public get; set; }



    /// TYPES

    /// Basic types
    Vector2 vec2 = new Vector2(-0.5f, 1f);
    Vector3 vec3 = new Vector3(0f, 1f, 2f);
    Vector3 up = Vector3.up; // same as Vector3(0f, 1f, 0f); Other ones like down, forward, zero etc exist

    // A quaternion is like a 4D Vector that represents 3D Rotations
    Quaternion noRotation = Quaternion.identity; // This is like the "0" of quaternions, rotating by it does nothing
    Quaternion rot = Quaternion.Euler(0f, 90f, 0f); // Define the quaternion with euler angles pitch, yaw, and roll

    Vector3 origin = Vector3.zero;
    Vector3 direction = Vector3.back;
    // Used in raycasts, a ray is like a laser that is shot out in a direction from some point
    ///       new Ray(origin      , direction   );
    Ray ray = new Ray(Vector3.zero, Vector3.back);

    // Objects exist on different layers, which is useful because we can decide how layers interact with eachother
    LayerMask worldMask; // We can use this to tell a raycast or collision check "Only check objects on this layer!"

    /// Scene objects
    Transform body; // Reference to a transform, contains Vector3 position, Quaternion rotation, and Vector3 scale
    GameObject obj; // A gameObject in the scene. It has a transform, and whatever fields you can see on it in the inspector
    Rigidbody rb; // A rigidbody tells an object it can interact with physics, you can do things like set the rb.linearVelocity
    Collider col; // The hitbox of an object
    
    // NOTE: all of these are by default 3D. For collisions 2D versions are needed
    Rigidbody2D rb2D;
    Collider2D col2D;



    /// LIFETIME METHODS (exist thanks to MonoBehaviour)

    // Awake is called when the script instance is loaded EVEN if the MonoBehaviour is disabled
    private void Awake()
    {
        /// ----------
        /// IMPORTANT: Debug.Log() is your BEST FRIEND FOR DEBUGGING. It prints whatever is inside to the console 
        /// DOES NOT HAVE TO BE A STRING
        /// Like System.out.println() in Java, except BETTER!!!
        /// ----------
        Debug.Log("Awake");
        

        // gameObject and transform are SPECIAL VARIABLES that refer to the object this script is ATTACHED TO
        // So like gameObject is this script's parent, and transform is its in-game position
        body = transform;
        obj = gameObject;

        // GetComponent<T>() searches THIS GameObject
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // If you want a component from another object:
        if (crate != null)
        {
            Rigidbody crateRb = crate.GetComponent<Rigidbody>();
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // It is ONLY called if it is enabled, so the moment you enable it, Start() is run
    private void Start()
    {
        Debug.Log("Start");

        // Good place for initialization logic
        enemyHealth = 100;

        // You can also find objects dynamically (less efficient than drag-and-drop)
        GameObject foundPlayer = GameObject.Find("Player");
        if (foundPlayer != null)
        {
            playerTransform = foundPlayer.transform;
        }
    }

    // Update is called once per frame
    /// The framerate is NOT CONSTANT
    private void Update()
    {
        Debug.Log("Update");

        /// Time.deltaTime is the time (in seconds) since the last frame happened
        // For smooth/continuous operations, multiply it to the operation to balance out the framerate
        // Otherwise people with high framerates will have this method called more often than people with low framerates

        // Ex: move the player forward constantly
        transform.position += Vector3.forward * speed * Time.deltaTime;

        // Rotate constantly around the y-axis
        transform.Rotate(Vector3.up * 90f * Time.deltaTime);
    }

    // Update is called once every fixed interval (50 times/second)
    // Used for constant movement so you dont need to deal with the inconsistencies of Time.deltaTime
    // You still have Time.fixedDeltaTime though for a measure of the interval
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");

        // Things relating to physics usually go here

        // If using Rigidbody physics, modify it here:
        if (rb != null)
        {
            // Change the velocity of the object (not position!!)
            rb.linearVelocity = Vector3.forward * speed;
        }

        // If you want frame-independent timing here:
        float fixedTimeStep = Time.fixedDeltaTime;
    }

    /// Collision
    // Called when this collider enters another collider
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
    }

    // Trigger version (when "Is Trigger" is checked, makes this collider pass-through)
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("This collider triggered by: " + other.gameObject.name);
    }



    /// CUSTOM METHODS

    // THis method is a short one-liner, so it can be expresed using arrow notation
    private void CompactMethod() => Debug.Log("Compact!");

    // Heres a method that adds then removes an object from the current scene
    private void AddThenRemoveObject(GameObject obj)
    {
        // Add an object to the scene at the given position and rotation (in this case no rotation)
        GameObject newObj = Instantiate(
            obj, 
            transform.position + new Vector3(-1.5f, 0f, 2f), 
            Quaternion.identity
        );

        // Remove it from the scene right after (tragically)
        Destroy(newObj);
    }

    // Heres a method that accesses a method in another script
    private void AccessAnotherScript()
    {
        // If another script is on the same object:
        // (If not youll need an inspector reference, see [SerializeField])
        OtherScript other = GetComponent<OtherScript>();

        if (other != null)
        {
            other.SomePublicMethod();
        }
    }

    /// That should be all! See Templates > Scripts for more tutorials on other features!
}
