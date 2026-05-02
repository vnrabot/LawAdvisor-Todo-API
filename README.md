# LawAdvisor Todo API

A high-performance, RESTful Todo API built with .NET 9 and C#, utilizing Clean Architecture principles. This API is specifically engineered to handle massive datasets and high-frequency UI drag-and-drop operations without performance degradation.

## 🏗 Architecture & Design Patterns
This project strictly follows **Clean Architecture**, separating the application into Domain, Application, Infrastructure, and API presentation layers. This ensures the core logic remains completely decoupled from Entity Framework and UI frameworks.

*   **Database:** SQLite (via Entity Framework Core)
*   **Dependency Injection:** Interfaces (e.g., `ITaskRepository`, `ITaskService`) are heavily utilized to maintain the S.O.L.I.D. Open-Closed and Dependency Inversion principles.

## 🚀 The Drag-and-Drop Engine (LexoRank + Merge Sort)
To satisfy the constraints of 1 million records and 50+ sequential drag-and-drop moves with sub-second response times, this API implements a custom **LexoRank + Merge Sort** strategy, intentionally avoiding standard `.OrderBy()` libraries.

**1. The Database Write Strategy ($O(1)$ Time)**
Traditional sequential integer sorting requires cascading database updates (updating 100,000 rows when one item moves). To avoid this, tasks are assigned lexicographical string values (`Rank`). When a task is reordered, the API dynamically generates a new string alphabetically between its new neighbors (e.g., dropping a task between `a` and `b` generates `am`). This guarantees that only **one single row** is updated per drag-and-drop action, keeping database writes lightning fast regardless of total row count.

**2. The In-Memory Read Strategy ($O(n \log n)$ Time)**
When retrieving the list, the data is passed through a custom generic `ISortingStrategy`. The implemented `LexoRankMergeSort` uses the Divide and Conquer method with `string.CompareOrdinal()` to rapidly alphabetize the tasks in memory before returning the payload to the client.

## 🛠 Setup & Installation

**Prerequisites:**
* .NET 9.0 SDK

**Run the Application:**
1. Clone the repository.
2. Open a terminal in the root directory.
3. Build the project and restore dependencies:
   ```bash
   dotnet build