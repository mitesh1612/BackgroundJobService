# BackgroundJobService

A PoC of implementing a basic BackgroundJobService from Scratch. The code doesnt rely on any particular document storage and queue service for its operation and exposes basic interfaces for working with such data providers. A sample in-memory/file system based implementation is also provided for demo purposes.

## Future/Pending Work

1. Fix Job Metadata Passing. Right now requires all new job callbacks to implement a certain constructor. Needs to be fixed.
2. Allow jobs to consume any injected service in the service collection.
3. Add more features like scheduling jobs.
4. More documentation in the code and on how to consume the code as well.