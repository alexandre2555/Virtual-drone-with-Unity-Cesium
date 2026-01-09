# Paris Drone Flight - VR Immersive Experience

> **Mini-Project:** Immersive Virtual Reality Experience with Unity & Cesium  
> **Team:** Pair Project  
> **Status:** Phase 1 Completed (Drone Physics & Environment) / Phase 2 (VR Implementation) Pending

## About The Project

This project aims to develop an immersive virtual reality experience representing a real georeferenced environment (Paris) using **Unity** and **Cesium**. 

The goal is to allow users to fly over a high-resolution 3D rendering of the Earth using a virtual drone. The project leverages **Cesium for Unity** integrated with **Cesium Ion** to stream cloud-based global content, including photogrammetry, terrain, imagery, and 3D buildings.

### Key Objectives
* Use **Unity** as the XR Engine.
* Integrate **Cesium for Unity** for real-world location data (Paris).
* Implement realistic drone physics and controls.
* *(Upcoming)* Integrate **Meta Quest 2** for full standalone VR immersion.

---

## Controls & Input

The drone is controlled using a keyboard (Simulating the future VR controller inputs).

| Action | Key / Input | Description |
| :--- | :--- | :--- |
| **Move Forward/Back** | `W` / `S` (or Arrow Up/Down) | Moves the drone horizontally and tilts the body forward/backward. |
| **Strafe Left/Right** | `A` / `D` (or Arrow Left/Right) | Moves the drone sideways (Swerving) and tilts the body. |
| **Ascend (Throttle Up)** | `I` | Increases vertical force to fly up rapidly. |
| **Descend (Throttle Down)** | `K` | Reverses vertical force to descend. |
| **Rotate (Yaw)** | `J` / `L` | Rotates the drone Left or Right on its axis. |
| **Hover** | *No Input* | The drone automatically stabilizes and hovers in place. |

---

## Technical Features (Drone Physics)

The drone behavior is governed by the `DroneMovementScript.cs`. Here is a breakdown of the implemented mechanics:

### 1. Levitation & Vertical Movement
* **Logic:** The script applies a constant `AddRelativeForce(Vector3.up)` to counteract gravity.
* **Dynamic Force:** The `upForce` variable changes dynamically based on input:
    * **Hovering:** Applies ~98.1f force to stay stationary.
    * **Ascending:** Increases force to 450-500 depending on horizontal velocity.
    * **Turning:** Applies specific force compensation (410) when rotating keys (`J` or `L`) are pressed to prevent losing altitude while banking.

### 2. Forward Movement & Tilting
* **Logic:** When moving forward or backward, the script applies force along the `Vector3.forward` axis.
* **Visual Feedback:** It uses `Mathf.SmoothDamp` to smoothly interpolate the drone's rotation (`tiltAmountForward`), simulating the aerodynamic tilt of a real drone accelerating.

### 3. Swerving (Strafing)
* **Logic:** Handled by the `Swerwe()` function. It applies force along the `Vector3.right` axis.
* **Banking:** Similar to forward movement, the drone tilts sideways (`tiltAmountSideways`) when strafing to enhance realism.

### 4. Rotation (Yaw)
* **Logic:** The `Rotation()` function modifies the `wantedYRotation` variable when `J` or `L` are pressed.
* **Smoothing:** The actual rotation is applied using `Mathf.SmoothDamp` to create a fluid turning motion rather than a snappy, robotic turn.

### 5. Speed Clamping (Safety Limits)
* **Logic:** The `ClampingSpeedValues()` function ensures the drone doesn't accelerate infinitely.
* **Implementation:** It limits the `linearVelocity` magnitude:
    * **Max Speed:** 10.0f when moving forward/diagonal.
    * **Precise Speed:** 5.0f when making fine side adjustments.
    * **Braking:** Applies a dampening effect when no keys are pressed to bring the drone to a smooth stop.

### 6. Rotor Animation
* **Script:** `rotor.cs`
* **Logic:** Controls the visual spinning of the propellers. It rotates the blades on the Y-axis based on a `power` variable. It supports counter-clockwise rotation for realistic physics representation (though purely visual in this implementation).

---

## Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/YourUsername/YourRepoName.git](https://github.com/YourUsername/YourRepoName.git)
    ```
2.  **Open in Unity:**
    * Launch **Unity Hub**.
    * Click **Add** and select the cloned folder.
    * *Note: This project requires a valid Cesium Ion API Key.*
3.  **Cesium Setup:**
    * If prompted, log in to your **Cesium Ion** account inside the Unity Editor to enable the 3D Tiles streaming.
    * **Location Setup:** If you need to reset or change the location, select the **CesiumGeoreference** object in the scene and set the **Origin Coordinates** (Latitude/Longitude) to Paris (e.g., Lat: `48.8566`, Lon: `2.3522`).
4.  **Play:**
    * Open the main Scene (e.g., `ParisScene.unity`).
    * Press the **Play** button in the editor.

---

## Future Roadmap

* [ ] **VR Integration:** Implement XR Interaction Toolkit to map keyboard controls to Meta Quest 2 Touch Controllers.
* [ ] **First Person View (FPV):** Add a camera to the drone for a "cockpit" view in VR.
* [ ] **Collision Handling:** Improve collision logic with buildings using mesh colliders.

---

## Credits

* **Engine:** Unity Technologies
* **Geospatial Data:** Cesium for Unity
* **Drone Asset:** Realistic Drone (Free Asset from Unity Asset Store)
