import statistics
 
sample = [2, 3, 3, 4, 5, 5, 5, 5, 6, 6, 6, 7]
 
print(f"입력 리스트={sample}")
print(f"평균={statistics.mean(sample)}")
print(f"중간값={statistics.median(sample)}")
print(f"최빈값={statistics.mode(sample)}")
print(f"표준편차={statistics.stdev(sample)}")
input()