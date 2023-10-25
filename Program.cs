using System;
using System.Web.Http;
using DigitalPersona.UareU; // Adjust the namespace to match your SDK version

public class FingerprintController : ApiController
{
    private ReaderCollection readers;

    public FingerprintController()
    {
        try
        {
            // Initialize the fingerprint reader
            readers = ReaderCollection.GetReaders();
            if (readers.Count == 0)
            {
                throw new Exception("No fingerprint reader found.");
            }
        }
        catch (Exception ex)
        {
            // Handle initialization errors
            throw ex;
        }
    }

    [HttpPost]
    public IHttpActionResult CaptureFingerprint()
    {
        try
        {
            // Get the first available fingerprint reader
            Reader reader = readers[0];
            
            // Open the reader
            reader.Open(Reader.Priority.Cooperative);

            // Capture the fingerprint
            Sample sample = reader.Capture(
                Finger.Primary, CaptureProcessing.Default);

            if (sample != null)
            {
                // Process the captured fingerprint data
                // You can use sample.Serialize() to work with the raw data

                // Close the reader
                reader.Close();

                return Ok("Fingerprint captured successfully.");
            }
            else
            {
                return BadRequest("Fingerprint capture failed.");
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during fingerprint capture
            return BadRequest("Fingerprint capture error: " + ex.Message);
        }
    }
}
