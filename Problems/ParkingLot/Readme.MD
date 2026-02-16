Patterns used:

Strategy (ISpotAssignmentStrategy)
“I can swap allocation policies (best-fit, nearest, reserved, EV-only) without changing the orchestrator.”

Observer (AvailabilityPublisher)
“Real-time availability updates can feed display boards, APIs, websockets, etc.”

Facade / Orchestrator (ParkingLotService)
“Keeps entry/exit flows in one place; domain objects stay small.”

Concurrency design (per-level ReaderWriterLockSlim)
“Multiple entry/exit points hit the same service concurrently; level acts as a locking boundary to prevent double-booking.”