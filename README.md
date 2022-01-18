# BackgroundJobService

A PoC of implementing a basic BackgroundJobService from Scratch. The code doesnt rely on any particular document storage and queue service for its operation and exposes basic interfaces for working with such data providers. A sample in-memory/file system based implementation is also provided for demo purposes.

**FIXME:** Right now the job queueing and dispatching works, but passing job metadata doesnt work. That said, this still works for jobs that dont require any metadata.

## Future/Pending Work

1. Fix Job Metadata Passing
2. Allow jobs to consume any injected service in the service collection.
3. Add more features like scheduling jobs.
4. More documentation in the code and on how to consume the code as well.