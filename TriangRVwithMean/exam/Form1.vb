Public Class Form1
    Public rnd As New Random
    'Parameters for the triangular distribution, as found on wiki
    Public a As Double = 10
    Public b As Double = 50
    Public c As Double = 11
    'Numbers extracted for each extraction
    Public NumberOfExtractions As List(Of Double)
    'Number of single extractions (should be the "N random variable")
    Public Extractions As Integer = 1
    'Classes to represent extracted numbers and their mean in a histogram
    Public ExtractionClasses As List(Of Double)
    Public MeanClassess As List(Of Double)

    Public WithEvents timerAnimazione As New Timer

    Private Sub TriangualrDistribution()

        NumberOfExtractions = New List(Of Double)

        Dim iterations As Integer = Extractions * 100
        Dim average As Double = 0
        Dim AverageForIterations As New List(Of Double)

        Dim F As Double
        Dim X As Double

        F = (c - a) / (b - a)

        For j As Integer = 1 To iterations
            average = 0
            For i As Integer = 1 To Extractions
                Dim U As Double = rnd.NextDouble
                If U < F Then
                    X = a + Math.Sqrt(U * (b - a) * (c - a))
                ElseIf U > F AndAlso U < 1 Then
                    X = b - Math.Sqrt((1 - U) * (b - a) * (b - c))
                End If
                NumberOfExtractions.Add(X)
                average += X
            Next
            average /= Extractions
            AverageForIterations.Add(average)
        Next

        ' Before drawing stuff we must switch to the classes (classes number i user defined)

        Dim valueC1 As Double() = ExtractClasses(50, NumberOfExtractions)
        Dim valueC2 As Double() = ExtractClasses(50, AverageForIterations)

        'Drawing part
        Chart1.Series("Series1").Points.Clear()
        Chart2.Series("Series1").Points.Clear()
        For Each el In valueC1
            Chart1.Series("Series1").Points.AddXY("", el)
        Next

        For Each el In valueC2
            Chart2.Series("Series1").Points.AddXY("", el)
        Next

    End Sub

    'Function to extract classes from a list of numbers (distribution) given the number of classes and the list
    Function ExtractClasses(NumberOfClasses As Integer, ListOfValues As List(Of Double)) As Double()
        Dim MinV As Double = Double.MaxValue
        Dim MaxV As Double = Double.MinValue
        ' Find minimum and maximum value in the given list
        MinV = ListOfValues.Min
        MaxV = ListOfValues.Max

        ' This is a list
        Dim Frequencies(NumberOfClasses - 1) As Double

        Dim Range As Double = MaxV - MinV
        Dim ClassAmplitude = Range / NumberOfClasses

        For Each value As Double In ListOfValues
            Dim IndexOfClass As Integer = Math.Floor((value - MinV) / ClassAmplitude)  '??check!
            If IndexOfClass > NumberOfClasses - 1 Then IndexOfClass = NumberOfClasses - 1

            Frequencies(IndexOfClass) += 1

        Next

        ExtractClasses = Frequencies

    End Function

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.timerAnimazione.Interval = 250
        Me.timerAnimazione.Start()

    End Sub

    Private Sub timerAnimazione_Tick(sender As Object, e As EventArgs) Handles timerAnimazione.Tick

        Me.TriangualrDistribution()
        Extractions += 1


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.timerAnimazione.Stop()
    End Sub
End Class
