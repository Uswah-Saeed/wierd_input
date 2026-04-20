# Pirate Puncture

An alternative controller system that transforms a discarded paper puncture tool into a functional catapult interface for digital gameplay.

🔗 Repository: https://github.com/Uswah-Saeed/wierd_input

---

## Overview

Pirate Puncture is an experimental interaction project that explores how physical objects can be repurposed into expressive game controllers. The system reimagines a flea market paper puncture device as a catapult mechanism, allowing players to defend the historic city of Lisbon from an invading pirate ship.

Instead of relying on conventional input devices, the project integrates embedded sensing into a mechanical object, turning it into the primary interface. The interaction is grounded in physical action, where force, tension, and release directly shape gameplay outcomes.

---

## Design Questions

This project investigates how interaction design changes when control is shifted from abstract inputs to embodied, physical actions.

Key questions explored include:

- How can everyday mechanical objects be reinterpreted as meaningful input devices?
- What forms of feedback emerge when interaction is constrained by physical affordances?
- How does tactile resistance and motion influence precision and player engagement?
- Can low-cost, repurposed materials support expressive and reliable interaction systems?

The work positions the controller not as a neutral intermediary, but as an active component of the experience.

---

## Interaction Design

The paper puncture device is modified to behave like a catapult:

- Pressing and tensioning the mechanism builds potential energy
- Releasing it triggers projectile launch in-game
- The physical resistance of the device directly affects timing and control

This creates a continuous coupling between bodily action and digital response, producing an interaction that is closer to operating a real-world mechanism than pressing a button.

---

## Hardware System

The controller integrates sensing components into the physical structure:

- Accelerometer for detecting motion and orientation
- Magnetometer for directional input
- Microcontroller (Arduino-based) for signal processing
- Serial communication pipeline to Unity

The sensors are embedded within the device, preserving its original form while extending its functionality.

---

## Software System

- **Engine:** Unity (C#)
- **Input Handling:** Custom serial communication layer
- **Gameplay System:** Projectile mechanics driven by physical input values

Sensor data is continuously mapped to gameplay parameters such as angle, force, and timing, ensuring that the physical action directly informs the digital outcome.

---

## Design Approach

The project follows a design-led research approach, where knowledge is generated through making and iteration.

Rather than optimizing for efficiency or precision alone, the focus is on:

- Material qualities of interaction (weight, resistance, motion)
- Embodied engagement
- Expressiveness over abstraction

The use of a found object introduces constraints that shape both the design and the resulting interaction.

---

## Outcome

Pirate Puncture demonstrates that:

- Functional game controllers can emerge from non-digital objects
- Physical affordances can meaningfully structure interaction
- Low-cost hardware setups can produce rich, embodied experiences

It contributes to ongoing exploration in alternative input systems and tangible interaction design.

---

## Future Directions

- Expanding the range of physical controllers using found objects
- Comparing player performance and engagement across input types
- Investigating accessibility through tactile-first interaction systems
- Applying similar approaches to educational or collaborative contexts

---

## Credits

Developed as part of an experimental exploration in alternative controller design.

Roles included:
- Interaction Design
- Hardware Integration
- Programming (Unity + Arduino)

---
