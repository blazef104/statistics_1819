﻿Public Class Form1

    Public rnd As New Random
    'Numbers extracted for each extraction
    Public NumberOfExtractions As List(Of Double)
    'Number of single extractions (should be the "N random variable")
    Public Extractions As Integer = 2
    'Classes to represent extracted numbers and their mean in a histogram
    Public ExtractionClasses As List(Of Double)
    Public MeanClassess As List(Of Double)

    Public WithEvents timerAnimazione As New Timer

    Private Sub NormalDistribution()

        NumberOfExtractions = New List(Of Double)
        'List to save the average for each iteration
        Dim AverageForIterations As New List(Of Double)
        'List to save the variance for each iteration
        Dim VarianceForIterations As New List(Of Double)
        Dim StandardDeviationForIterations As New List(Of Double)
        Dim iterations As Integer = Extractions * 100
        Dim average As Double = 0
        Dim variance As Double = 0
        Dim N As Double

        For j As Integer = 1 To iterations
            average = 0
            variance = 0

            For i As Integer = 1 To Extractions
                Dim U1 As Double = rnd.NextDouble
                Dim U2 As Double = rnd.NextDouble
                N = Math.Sqrt(-2 * Math.Log(U1)) * Math.Cos(2 * Math.PI * U2)
                'There sould be the one with cos
                'Dim N As Double = Math.Sqrt(-2 * Math.Log(U1)) * Math.Sin(2 * Math.PI * U2)
                NumberOfExtractions.Add(N)
                average += N
            Next
            average /= Extractions
            variance = Math.Pow(average, 2) * (Extractions - 1)
            VarianceForIterations.Add(variance)
            StandardDeviationForIterations.Add(Math.Sqrt(variance))
            AverageForIterations.Add(average)
        Next

        ' Before drawing stuff we must switch to the classes (classes number i user defined)

        Dim valueC1 As Double() = ExtractClasses(50, NumberOfExtractions)
        Dim valueC2 As Double() = ExtractClasses(50, AverageForIterations)
        Dim valueC3 As Double() = ExtractClasses(50, VarianceForIterations)
        Dim valueC3_1 As Double() = ExtractClasses(50, StandardDeviationForIterations)
        'Drawing part
        Chart1.Series(0).Points.Clear()
        Chart2.Series(0).Points.Clear()
        Chart3.Series(0).Points.Clear()
        Chart3.Series(1).Points.Clear()

        For Each el In valueC1
            Chart1.Series(0).Points.AddXY("", el)
        Next
        For Each el In valueC2
            Chart2.Series(0).Points.AddXY("", el)
        Next
        For Each el In valueC3
            Chart3.Series(0).Points.AddXY("", el)
        Next
        For Each el In valueC3_1
            Chart3.Series(1).Points.AddXY("", el)
        Next

    End Sub

    'Function to extract classes from a list of numbers (distribution) given the number of classes and the list
    Function ExtractClasses(NumberOfClasses As Integer, ListOfValues As List(Of Double)) As Double()
        Dim MinV As Double = Double.MaxValue
        Dim MaxV As Double = Double.MinValue
        ' Find minimum and maximum value in the given list
        MaxV = ListOfValues.Max
        MinV = ListOfValues.Min


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

        Me.NormalDistribution()
        Extractions += 1
        Label1.Text = "N: " + Extractions.ToString

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.timerAnimazione.Stop()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Chart3_Click(sender As Object, e As EventArgs) Handles Chart3.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub
End Class
