# BackgroundJobService

A PoC of implementing a basic BackgroundJobService from Scratch. The code doesnt rely on any particular document storage and queue service for its operation and exposes basic interfaces for working with such data providers. A sample in-memory/file system based implementation is also provided for demo purposes.

## Future/Pending Work

1. Allow jobs to consume any injected service in the service collection.
2. Add more features like scheduling jobs.
3. More documentation in the code and on how to consume the code as well. Add ADR and some design info.