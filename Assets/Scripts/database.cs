using Firebase.Firestore;

[FirestoreData]
public class database
{
    [FirestoreProperty]
    public int Count { get; set; }
    
    [FirestoreProperty]
    public string UpdatedBy { get; set; }
}
